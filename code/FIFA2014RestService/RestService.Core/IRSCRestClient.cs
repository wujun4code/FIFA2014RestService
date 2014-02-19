using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestService.Core
{
    public interface IRSCRestClient
    {
        IEnumerable<T> GetFromUrl<T>(string BaseUrl, string Resource);

        T Create<T>(string BaseUrl, string Resource,T entity);
    }
}
