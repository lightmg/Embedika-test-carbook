namespace CarsCatalog.Controllers.Models
{
    public abstract class AddRequestModelBase<TDbModel>
    {
        public abstract bool IsSameWith(TDbModel model);
        public abstract TDbModel ToDbModel();
    }
}