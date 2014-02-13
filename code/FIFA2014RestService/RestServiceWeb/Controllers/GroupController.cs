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
    public class GroupController : BaseApiController<string,Group>
    {
        IGroup db = new SimpleGroupService();
        
        #region logic relation BLL

        [Route("api/group/{groupID}/teams")]
        public IEnumerable<DataWrapper<Team>> GetAllTeamsInCurrentGroup(string groupID)
        {
            var rtn = new List<DataWrapper<Team>>();

            return rtn;
        }

        [Route("api/group/{groupID}/matches")]
        public IEnumerable<DataWrapper<Match>> GetAllMatchesInCurrentGroup(string groupID)
        {
            var rtn = new List<DataWrapper<Match>>();

            return rtn;
        }

        #endregion

    }
}
