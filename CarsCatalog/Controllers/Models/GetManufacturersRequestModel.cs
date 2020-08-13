using CarsCatalog.DbModels;

namespace CarsCatalog.Controllers.Models
{
    public class GetManufacturersRequestModel : GetRequestModelBase<CarManufacturer>
    {
        public string[] Names { get; set; }
        public ulong[] Ids { get; set; }
    }
}