using System;
using System.Linq;

namespace CarsCatalog.Db
{
    public class InMemoryEntityStorage : IEntityStorage
    {
        public InMemoryEntityStorage()
        {
            StorageImpl ??= new MemoryStorageImpl();
        }

        protected MemoryStorageImpl StorageImpl { get; set; }

        public IQueryable<T> Select<T>() where T : BaseDbEntity =>
            StorageImpl.GetTable(typeof(T))
                .Cast<T>()
                .AsQueryable();

        public void Remove<T>(ulong id) where T : BaseDbEntity =>
            StorageImpl.Remove(typeof(T), id);

        public void Add<T>(T entity) where T : BaseDbEntity =>
            StorageImpl.Add(entity);

        public DbStats GetStats()
        {
            var firstRecordTime = DateTime.MaxValue;
            var lastRecordTime = DateTime.MinValue;
            var totalCount = 0L;
            foreach (var entity in StorageImpl.GetAllEntities())
            {
                if (entity.CreationTime < firstRecordTime)
                    firstRecordTime = entity.CreationTime;
                if (entity.CreationTime > lastRecordTime)
                    lastRecordTime = entity.CreationTime;
                totalCount++;
            }

            return new DbStats
            {
                TablesCount = StorageImpl.TablesCount,
                RecordsCount = totalCount,
                FirstRecordTime = totalCount == 0 ? (DateTime?) null : firstRecordTime,
                LastRecordTime = totalCount == 0 ? (DateTime?) null : lastRecordTime
            };
        }
    }
}