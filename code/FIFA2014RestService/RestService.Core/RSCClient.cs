using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestService.Core
{
    public class RSCClient : IRSCRestClient
    {
        IRestClient Client = new RestClient();
        public IEnumerable<T>  GetFromUrl<T>(string BaseUrl, string Resource)
        {
            var req = new RestRequest();

            Client.BaseUrl = BaseUrl;

            req.Method = Method.GET;

            req.Resource = Resource;

            var rep = Client.Execute(req);

            var result = SimpleJson.DeserializeObject<IEnumerable<DataWrapper<T>>>(rep.Content);

            List<T> rtn = new List<T>();
            foreach (var dt in result)
            {
                rtn.Add(dt.Entity);
            }
            return rtn;
        }
        public T Create<T>(string BaseUrl, string Resource, T entity)
        {
            var req = new RestRequest();

            Client.BaseUrl = BaseUrl;

            req.Method = Method.POST;

            req.Resource = Resource;

            var BodyDataString = SimpleJson.SerializeObject(entity);

            req.AddParameter("application/json", BodyDataString, ParameterType.RequestBody);

            var rep = Client.Execute(req);

            var rtn = SimpleJson.DeserializeObject<DataWrapper<T>>(rep.Content);

            return rtn.Entity;
        }
       
    }
}
