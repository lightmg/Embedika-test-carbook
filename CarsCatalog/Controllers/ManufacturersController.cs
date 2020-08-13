using System.Collections.Generic;
using System.Linq;
using CarsCatalog.Controllers.Models;
using CarsCatalog.Db;
using CarsCatalog.DbModels;
using CarsCatalog.Helpers;

namespace CarsCatalog.Controllers
{
    public class ManufacturersController
        : BaseCrudController<CarManufacturer, AddManufacturerRequestModel, GetManufacturersRequestModel>
    {
        public ManufacturersController(IEntityStorage entityStorage) : base(entityStorage)
        {
        }

        protected override IEnumerable<CarManufacturer> FilterQuery(IQueryable<CarManufacturer> query,
            GetManufacturersRequestModel model) => query
            .If(!model.Ids.IsNullOrEmpty(), q => q.Where(m => model.Ids.Contains(m.Id)))
            .If(!model.Names.IsNullOrEmpty(), q => q.Where(m => model.Names.Contains(m.Name)));
    }
}