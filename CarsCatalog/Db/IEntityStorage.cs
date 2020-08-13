using System.Linq;

namespace CarsCatalog.Db
{
    public interface IEntityStorage
    {
        IQueryable<T> Select<T>() where T : BaseDbEntity;
        void Remove<T>(ulong id) where T : BaseDbEntity;
        void Add<T>(T entity) where T : BaseDbEntity;
        DbStats GetStats();
    }
}