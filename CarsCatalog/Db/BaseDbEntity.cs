using System;

namespace CarsCatalog.Db
{
    public abstract class BaseDbEntity : ICloneable<BaseDbEntity>
    {
        public ulong Id { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual BaseDbEntity Clone() => (BaseDbEntity) MemberwiseClone();
    }
}