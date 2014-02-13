using BaaSReponsitory;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServiceWeb.BLL.ServiceContracts
{
    public interface IGroup : IBaaS<string, Group>
    {

    }
}