using CarsCatalog.Db;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.Controllers
{
    public class DbController : Controller
    {
        private readonly IEntityStorage entityStorage;

        public DbController(IEntityStorage entityStorage)
        {
            this.entityStorage = entityStorage;
        }

        public DbStats Stats()
        {
            return entityStorage.GetStats();
        }
    }
}