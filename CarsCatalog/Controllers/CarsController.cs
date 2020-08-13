using System.Collections.Generic;
using System.Linq;
using CarsCatalog.Controllers.Models;
using CarsCatalog.Db;
using CarsCatalog.DbModels;
using CarsCatalog.Helpers;

namespace CarsCatalog.Controllers
{
    public class CarsController : BaseCrudController<Car, AddCarRequestModel, GetCarsRequestModel>
    {
        public CarsController(IEntityStorage entityStorage) : base(entityStorage)
        {
        }

        protected override IEnumerable<Car> FilterQuery(IQueryable<Car> query, GetCarsRequestModel model)
        {
            if (model.FromDate.HasValue && model.ToDate.HasValue && model.FromDate > model.ToDate)
                return new Car[0];

            return query
                .If(!model.ManufacturerIds.IsNullOrEmpty(),
                    q => q.Where(c => model.ManufacturerIds.Contains(c.ManufacturerId)))
                .If(!model.HexColorCodes.IsNullOrEmpty(),
                    q => q.Where(c => model.HexColorCodes.Contains(c.HexColorCode)))
                .If(!model.CarIds.IsNullOrEmpty(), q => q.Where(c => model.CarIds.Contains(c.Id)))
                .If(!model.Models.IsNullOrEmpty(), q => q.Where(c => model.Models.Contains(c.Model)))
                .If(model.FromDate.HasValue, q => q.Where(c => c.ManufacturedDate > model.FromDate.Value))
                .If(model.ToDate.HasValue, q => q.Where(c => c.ManufacturedDate < model.ToDate.Value));
        }
    }
}