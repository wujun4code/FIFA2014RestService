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
    public abstract class BaseApiController<TKey, TEntity> : ApiController
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

        private IBaaSEntityRelation _baaSEntityRelation;
        public IBaaSEntityRelation EntityRelationService
        {
            get
            {
                if (_baaSEntityRelation == null)
                {
                    _baaSEntityRelation = new SimpleService();
                }
                return _baaSEntityRelation;
            }
            set
            {
                _baaSEntityRelation = value;
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
                rtnItem.ID = GetEntityId(g).ToString();
                rtn.Add(rtnItem);
            }
            return rtn;
        }


        public DataWrapper<TEntity> Get(TKey id)
        {
            var rtn = new DataWrapper<TEntity>();
            rtn.Entity = Db.Get<TKey, TEntity>(id);
            rtn.ID = id.ToString();
            return rtn;
        }

        public DataWrapper<TEntity> Post([FromBody]TEntity value)
        {
            var rtn = new DataWrapper<TEntity>();

            rtn.Entity = Db.Add<TKey, TEntity>(value);
            rtn.ID = GetEntityId(rtn.Entity).ToString();

            return rtn;
        }

        public void Put(TKey id, [FromBody]TEntity value)
        {
            SetTEntityId(value, id);
            Db.Update<TKey, TEntity>(value);
        }
        public void Delete(TKey id)
        {
            TEntity toBeDeleted = default(TEntity);
            SetTEntityId(toBeDeleted, id);
            Db.Delete<TKey, TEntity>(toBeDeleted);
        }


        protected virtual List<DataWrapper<T>> GetRelated<S, T>(string S_ID)
            where T : class
            where S : class
        {
            var rtn = new List<DataWrapper<T>>();
            var S_entity = Db.Get<string, S>(S_ID);
            var T_results = this.EntityRelationService.GetRelatedEntities<S, T>(S_entity);
            foreach (var T_entity in T_results)
            {
                var rtnItem = new DataWrapper<T>();
                rtnItem.Entity = T_entity;
                rtnItem.ID = GetEntityId<T>(T_entity).ToString();
                rtn.Add(rtnItem);
            }

            return rtn;
        }

        protected virtual List<DataWrapper<T>> GetRelated<S, T>(string S_ID, string PropertyName)
            where T : class
            where S : class
        {
            var rtn = new List<DataWrapper<T>>();
            var S_entity = Db.Get<string, S>(S_ID);
            var T_results = this.EntityRelationService.GetRelatedEntities<S, T>(S_entity, PropertyName);
            foreach (var T_entity in T_results)
            {
                var rtnItem = new DataWrapper<T>();
                rtnItem.Entity = T_entity;
                rtnItem.ID = GetEntityId<T>(T_entity).ToString();
                rtn.Add(rtnItem);
            }

            return rtn;
        }

        protected virtual string GetEntityId<T>(T entity)
        {
            var rtn = "";
            var type = typeof(T);

            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        rtn = pi.GetValue(entity).ToString();
                        break;
                    }
                }
            }
            return rtn;
        }

        protected virtual TKey GetEntityId(TEntity entity)
        {
            var rtn = default(TKey);
            var type = typeof(TEntity);

            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        rtn = (TKey)pi.GetValue(entity);
                        break;
                    }
                }
            }
            return rtn;
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