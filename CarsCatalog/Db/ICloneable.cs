namespace CarsCatalog.Db
{
    public interface ICloneable<out T>
    {
        T Clone();
    }
}