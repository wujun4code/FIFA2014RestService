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
    public class TeamController : BaseApiController<string, Team>
    {
        #region logic relation BLL

        [Route("api/team/{teamID}/players")]
        public IEnumerable<DataWrapper<Player>> GetAllPlayersInCurrentTeam(string teamID)
        {
            var rtn = new List<DataWrapper<Player>>();

            return rtn;
        }

        [Route("api/team/{teamID}/matches")]
        public IEnumerable<DataWrapper<Match>> GetAllMatchesInCurrentTeam(string teamID)
        {
            var rtn = new List<DataWrapper<Match>>();

            return rtn;
        }
        #endregion

    }
}