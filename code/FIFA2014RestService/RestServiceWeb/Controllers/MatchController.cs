using RestService.Core;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestServiceWeb.Controllers
{
    public class MatchController : BaseApiController<string, Match>
    {

        #region logic relation BLL
        [Route("api/match/{matchID}/result")]
        public DataWrapper<MatchResult> GetResultOfMatch(string matchID)
        {
            return this.GetMany2OneRelated<Match, MatchResult>(matchID);
        }

        [Route("api/match/{matchID}/result")]
        [HttpPost]
        public DataWrapper<MatchResult> SetResultOfMatch(string matchID, DataWrapper<MatchResult> macthResult)
        {
            return this.AssignRelated<Match, MatchResult>(matchID, macthResult);
        }

        #endregion
    }
}