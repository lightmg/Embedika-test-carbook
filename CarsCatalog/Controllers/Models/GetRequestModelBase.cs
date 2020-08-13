using CarsCatalog.Db;

namespace CarsCatalog.Controllers.Models
{
    public abstract class GetRequestModelBase<TEntity> where TEntity : BaseDbEntity
    {
        public string OrderByProperty { get; set; }
        public bool OrderByDescending { get; set; }
    }
}