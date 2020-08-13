using System;
using CarsCatalog.Db;

namespace CarsCatalog.DbModels
{
    public class Car :  BaseDbEntity
    {
        public string LicensePlateNumber { get; set; }
        public string Model { get; set; }
        public string HexColorCode { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public ulong ManufacturerId { get; set; }
    }
}