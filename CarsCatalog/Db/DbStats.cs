using System;

namespace CarsCatalog.Db
{
    public class DbStats
    {
        public DateTime? FirstRecordTime { get; set; }
        public DateTime? LastRecordTime { get; set; }
        public int TablesCount { get; set; }
        public long RecordsCount { get; set; }
    }
}