using BaaSReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestService.Core
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
        private ICloudObjectAnalyze _cloudObjectAnalyze;
        public ICloudObjectAnalyze CloudObjectAnalyze
        {
            get
            {
                if (_cloudObjectAnalyze == null)
                {
                    _cloudObjectAnalyze = new SimpleCloudObjectAnalyze();
                }
                return _cloudObjectAnalyze;
            }
            set
            {
                _cloudObjectAnalyze = value;
            }
        }

        //public virtual IEnumerable<DataWrapper<TEntity>> Get()
        //{
        //    var all = Db.GetAll<TKey, TEntity>();
        //    var rtn = CreateByTEntityCollection(all);
        //    return rtn;
        //}

        protected virtual IEnumerable<DataWrapper<TEntity>> CreateByTEntityCollection(IQueryable<TEntity> entities)
        {
            var rtn = new List<DataWrapper<TEntity>>();
            foreach (var e in entities)
            {
                var rtnItem = new DataWrapper<TEntity>();
                rtnItem.Entity = e;
                rtnItem.ID = GetEntityId(e).ToString();
                rtn.Add(rtnItem);
            }
            return rtn;
        }

        public virtual IEnumerable<DataWrapper<TEntity>> GetByFilter([FromUri]FilterWrapper filterWrapper)
        {
            IQueryable<TEntity> result = null;
            bool getAll = true;
            if (filterWrapper != null)
            {
                if (!string.IsNullOrEmpty(filterWrapper.where))
                {
                    result = Db.GetByFilter<TKey, TEntity>(filterWrapper.where);
                    getAll = false;
                }
            }
            if (getAll)
            {
                result = Db.GetAll<TKey, TEntity>();
            }

            var rtn = CreateByTEntityCollection(result);

            return rtn;
        }

        public virtual DataWrapper<TEntity> Get(TKey id)
        {
            var rtn = new DataWrapper<TEntity>();
            rtn.Entity = Db.Get<TKey, TEntity>(id);
            rtn.ID = id.ToString();
            return rtn;
        }

        public virtual DataWrapper<TEntity> Post([FromBody]TEntity value)
        {
            var rtn = new DataWrapper<TEntity>();

            rtn.Entity = Db.Add<TKey, TEntity>(value);
            rtn.ID = GetEntityId(rtn.Entity).ToString();

            return rtn;
        }

        public virtual void Put(TKey id, [FromBody]TEntity value)
        {
            SetTEntityId(value, id);
            Db.Update<TKey, TEntity>(value);
        }
        public virtual void Delete(TKey id)
        {
            TEntity toBeDeleted = default(TEntity);
            SetTEntityId(toBeDeleted, id);
            Db.Delete<TKey, TEntity>(toBeDeleted);
        }


        protected virtual DataWrapper<T> AssignRelated<S, T>(TKey S_ID, DataWrapper<T> T_wrapper)
            where T : class
            where S : class
        {
            string propertyName = string.Empty;
            var relationInfo = CloudObjectAnalyze.GetRealtionInfo<S, T>(out propertyName);
            return AssignRelated<S, T>(S_ID, propertyName, relationInfo, T_wrapper);
        }


        protected virtual DataWrapper<T> AssignRelated<S, T>(TKey S_ID, string PropertyName, DataWrapper<T> T_wrapper)
            where T : class
            where S : class
        {

            var relationInfo = CloudObjectAnalyze.GetRealtionInfo<S, T>(PropertyName);

            return AssignRelated<S, T>(S_ID, PropertyName, relationInfo, T_wrapper);

        }
        protected virtual DataWrapper<T> AssignRelated<S, T>(TKey S_ID, string PropertyName, CloudFiled RelatinInfo, DataWrapper<T> T_wrapper)
            where T : class
            where S : class
        {
            DataWrapper<T> rtn = default(DataWrapper<T>);
            if (!string.IsNullOrWhiteSpace(T_wrapper.ID))
            {
                SetTEntityId<T>(T_wrapper.Entity, T_wrapper.ID);
            }
            else
            {
                T_wrapper.Entity = Db.Add<TKey, T>(T_wrapper.Entity);
            }

            if (RelatinInfo.RelationType == CloudFiledType.OneToOne || RelatinInfo.RelationType == CloudFiledType.ManyToOne)
            {
                rtn = this.CreateMany2OneRelated<S, T>(S_ID, PropertyName, T_wrapper.Entity);
            }
            else
            {
                rtn = this.CreateOne2ManyRelated<S, T>(S_ID, PropertyName, T_wrapper.Entity);
            }

            return rtn;
        }
        protected virtual DataWrapper<T> CreateOne2ManyRelated<S, T>(TKey S_ID, string PropertyName, T T_entity)
            where T : class
            where S : class
        {
            var rtn = new DataWrapper<T>();
            var S_entity = Db.Get<TKey, S>(S_ID);
            this.EntityRelationService.AddOne2ManyRelation(S_entity, PropertyName, T_entity);
            Db.Update<TKey, S>(S_entity);
            rtn.Entity = T_entity;
            rtn.ID = GetEntityId<T>(T_entity).ToString();
            return rtn;
        }

        protected virtual DataWrapper<T> CreateMany2OneRelated<S, T>(TKey S_ID, string PropertyName, T T_entity)
            where T : class
            where S : class
        {
            var rtn = new DataWrapper<T>();
            var S_entity = Db.Get<TKey, S>(S_ID);
            SetTInS<S, T>(S_entity, PropertyName, T_entity);
            Db.Update<TKey, S>(S_entity);
            rtn.Entity = T_entity;
            rtn.ID = GetEntityId<T>(T_entity).ToString();
            return rtn;
        }

        protected virtual DataWrapper<T> GetMany2OneRelated<S, T>(TKey S_ID)
            where T : class
            where S : class
        {
            string propertyName = string.Empty;

            var relationInfo = CloudObjectAnalyze.GetRealtionInfo<S, T>(out propertyName);

            return this.GetMany2OneRelated<S, T>(S_ID, propertyName);
        }

        protected virtual DataWrapper<T> GetMany2OneRelated<S, T>(TKey S_ID, string PropertyName)
            where T : class
            where S : class
        {
            DataWrapper<T> rtn = new DataWrapper<T>();
            var S_entity = Db.Get<TKey, S>(S_ID);
            rtn.Entity = GetTInS<S, T>(S_entity, PropertyName);
            var T_ID = GetEntityId<TKey, T>(rtn.Entity);
            rtn.Entity = Db.Get<TKey, T>(T_ID);
            return rtn;
        }

        protected virtual List<DataWrapper<T>> GetOne2ManyRelated<S, T>(string S_ID)
            where T : class
            where S : class
        {
            string propertyName = string.Empty;

            var relationInfo = CloudObjectAnalyze.GetRealtionInfo<S, T>(out propertyName);

            return this.GetOne2ManyRelated<S, T>(S_ID, propertyName);
        }

        protected virtual List<DataWrapper<T>> GetOne2ManyRelated<S, T>(string S_ID, string PropertyName)
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
            return GetEntityId<string, T>(entity);
        }
        protected virtual TK GetEntityId<TK, TE>(TE entity)
        {
            var rtn = default(TK);
            var type = typeof(TE);

            var pro_infos = type.GetProperties();
            foreach (var pi in pro_infos)
            {
                var cloud_fields = pi.GetCustomAttributes(typeof(CloudFiled), true);

                if (cloud_fields.Length > 0)
                {
                    var cloud_field = cloud_fields[0];

                    if (((CloudFiled)cloud_field).IsPrimaryKey)
                    {
                        rtn = (TK)pi.GetValue(entity);
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

        protected virtual void SetTEntityId<T>(T entity, string objectId)
        {
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
                        pi.SetValue(entity, objectId);
                        break;
                    }
                }
            }
        }

        protected virtual void SetTInS<S, T>(S source, string PropertyName, T entity)
        {
            var T_type = typeof(T);
            var S_type = typeof(S);
            var pi = S_type.GetProperty(PropertyName);
            if (pi.PropertyType == T_type)
            {
                pi.SetValue(source, entity);
            }

        }

        protected virtual T GetTInS<S, T>(S source, string PropertyName)
        {
            var rtn = default(T);
            var T_type = typeof(T);
            var S_type = typeof(S);
            var pi = S_type.GetProperty(PropertyName);
            if (pi.PropertyType == T_type)
            {
                rtn = (T)pi.GetValue(source);
            }
            return rtn;
        }
    }
}