using System;
using CarsCatalog.DbModels;

namespace CarsCatalog.Controllers.Models
{
    public class GetCarsRequestModel : GetRequestModelBase<Car>
    {
        public ulong[] CarIds { get; set; }
        public ulong[] ManufacturerIds { get; set; }
        public string[] HexColorCodes { get; set; }
        public string[] Models { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}