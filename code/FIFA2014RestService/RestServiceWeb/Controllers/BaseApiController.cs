using BaaSReponsitory;
using RestServiceWeb.BLL.ServiceContracts;
using RestServiceWeb.BLL.ServiceImpls;
using RestServiceWeb.Models.DataContracts;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestServiceWeb.Controllers
{
    public abstract class BaseApiController<TKey,TEntity> : ApiController
        where TEntity : class
    {
        private IBaaSService _db;
        public IBaaSService Db
        {
            get 
            {
                if (_db == null)
                {
                    _db = new SimpleService();
                }
                return _db;
            }
            set
            {
                _db = value;
            }
        }
        public IEnumerable<DataWrapper<TEntity>> Get()
        {
            var rtn = new List<DataWrapper<TEntity>>();
            var all = Db.GetAll<TKey, TEntity>();
            foreach (var g in all)
            {
                var rtnItem = new DataWrapper<TEntity>();
                rtnItem.Entity = g;
                rtn.Add(rtnItem);
            }
            return rtn;
        }


        public DataWrapper<TEntity> Get(TKey id)
        {
            var rtn = new DataWrapper<TEntity>();
            rtn.Entity = Db.Get<TKey, TEntity>(id);
            return rtn;
        }

        public void Post([FromBody]TEntity value)
        {
            Db.Add<TKey, TEntity>(value);
        }

        public void Put(TKey id, [FromBody]TEntity value)
        {
            SetTEntityId(value, id);
            Db.Update<TKey, TEntity>(value);
        }
        public void Delete(TKey id)
        {
            TEntity toBeDeleted=default(TEntity);
             SetTEntityId(toBeDeleted, id);
             Db.Delete<TKey, TEntity>(toBeDeleted);
        }
        protected virtual void SetTEntityId(object entity, TKey objectId)
        {
            var type = entity.GetType();
            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        pi.SetValue(entity, objectId);
                        break;
                    }
                }
            }
        }
    }
}