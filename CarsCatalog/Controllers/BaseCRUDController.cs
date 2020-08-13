using System;
using System.Collections.Generic;
using System.Linq;
using CarsCatalog.Controllers.Models;
using CarsCatalog.Db;
using CarsCatalog.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.Controllers
{
    public abstract class BaseCrudController<TEntity, TAddRequestModel, TGetRequestModel> : Controller
        where TEntity : BaseDbEntity
        where TAddRequestModel : AddRequestModelBase<TEntity>
        where TGetRequestModel : GetRequestModelBase<TEntity>
    {
        protected readonly IEntityStorage EntityStorage;

        protected BaseCrudController(IEntityStorage entityStorage)
        {
            EntityStorage = entityStorage;
        }

        public TEntity[] Get(TGetRequestModel model)
        {
            var query = EntityStorage.Select<TEntity>();
            return model == null
                ? query.ToArray()
                : FilterQuery(query, model)
                    .If(!model.OrderByProperty.IsNullOrEmpty(),
                        q => q.OrderByPropertyName(model.OrderByProperty, model.OrderByDescending))
                    .ToArray();
        }

        [HttpPost]
        public AddResponseModel Add([FromBody] TAddRequestModel model)
        {
            if (model == null)
                return AddResponseModel.BadInput();
            if (EntityStorage.Select<TEntity>().Any(c => model.IsSameWith(c)))
                return AddResponseModel.AlreadyExists(typeof(TEntity).Name);
            var dbModel = model.ToDbModel();
            try
            {
                EntityStorage.Add(dbModel);
            }
            catch (Exception e)
            {
                return AddResponseModel.Failed(e);
            }

            return AddResponseModel.Successful(dbModel.Id);
        }

        public RemoveResponseModel Remove(ulong id)
        {
            if (!EntityStorage.Select<TEntity>().Any(c => c.Id == id))
                return RemoveResponseModel.NotExisting(id, typeof(TEntity).Name);
            try
            {
                EntityStorage.Remove<TEntity>(id);
            }
            catch (Exception e)
            {
                return RemoveResponseModel.Exceptional(e);
            }

            return RemoveResponseModel.Successful();
        }

        protected abstract IEnumerable<TEntity> FilterQuery(IQueryable<TEntity> query, TGetRequestModel model);
    }
}