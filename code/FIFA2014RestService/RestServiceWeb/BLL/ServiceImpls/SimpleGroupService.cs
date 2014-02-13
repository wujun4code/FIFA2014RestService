using BaaSReponsitory;
using RestServiceWeb.BLL.ServiceContracts;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServiceWeb.BLL.ServiceImpls
{
    public class SimpleGroupService : BaseService<string,Group>,IGroup
    {

    }
}