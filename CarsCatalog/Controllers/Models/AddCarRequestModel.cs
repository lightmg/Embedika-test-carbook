using System;
using CarsCatalog.DbModels;

namespace CarsCatalog.Controllers.Models
{
    public class AddCarRequestModel : AddRequestModelBase<Car>
    {
        public string LicensePlateNumber { get; set; }
        public string Model { get; set; }
        public string HexColorCode { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public ulong ManufacturerId { get; set; }

        public override bool IsSameWith(Car model)
        {
            return LicensePlateNumber == model.LicensePlateNumber &&
                   Model == model.Model &&
                   HexColorCode == model.HexColorCode &&
                   ManufacturedDate == model.ManufacturedDate &&
                   ManufacturerId == model.ManufacturerId;
        }

        public override Car ToDbModel() => new Car
        {
            LicensePlateNumber = LicensePlateNumber,
            Model = Model,
            HexColorCode = HexColorCode,
            ManufacturedDate = ManufacturedDate,
            ManufacturerId = ManufacturerId,
        };
    }
}