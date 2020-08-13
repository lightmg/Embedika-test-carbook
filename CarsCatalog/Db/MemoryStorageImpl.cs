using System;
using System.Collections.Generic;
using System.Linq;
using CarsCatalog.Helpers;
using Newtonsoft.Json;

namespace CarsCatalog.Db
{
    public class MemoryStorageImpl
    {
        [JsonRequired] private ulong lastId;

        [JsonRequired] private Dictionary<Type, Dictionary<ulong, BaseDbEntity>> entities =
            new Dictionary<Type, Dictionary<ulong, BaseDbEntity>>();

        [JsonIgnore] public int TablesCount => entities.Count;

        public IEnumerable<BaseDbEntity> GetTable(Type type)
        {
            return entities.GetValueOrDefault(type)
                    .EmptyIfNull()
                    .Select(x => x.Value);
        }

        public IEnumerable<BaseDbEntity> GetAllEntities()
        {
            return entities.SelectMany(x => x.Value.Select(y => y.Value));
        }

        public void Remove(Type type, ulong id)
        {
            if (entities.TryGetValue(type, out var collection))
                collection.Remove(id);
            OnUpdated?.Invoke();
        }

        public void Add(BaseDbEntity entity)
        {
            entity.Id = lastId++;
            entity.CreationTime = DateTime.Now;
            entities.GetOrAdd(entity.GetType(), () => new Dictionary<ulong, BaseDbEntity>())
                .Add(entity.Id, entity.Clone());
            OnUpdated?.Invoke();
        }

        public event Action OnUpdated;
    }
}