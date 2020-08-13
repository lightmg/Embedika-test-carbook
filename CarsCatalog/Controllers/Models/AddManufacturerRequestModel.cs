using CarsCatalog.DbModels;

namespace CarsCatalog.Controllers.Models
{
    public class AddManufacturerRequestModel : AddRequestModelBase<CarManufacturer>
    {
        public string Name { get; set; }

        public override bool IsSameWith(CarManufacturer model) => model.Name == Name;
        public override CarManufacturer ToDbModel() => new CarManufacturer {Name = Name};
    }
}