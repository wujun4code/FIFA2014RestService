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
            DataWrapper<MatchResult> rtn = new DataWrapper<MatchResult>();
            var S_entity = Db.Get<string, Match>(matchID);
            rtn.Entity = S_entity.Result;
            return rtn;
        }

        [Route("api/match/{matchID}/result")]
        [HttpPost]
        public DataWrapper<MatchResult> GetResultOfMatch(string matchID, DataWrapper<MatchResult> result)
        {
            DataWrapper<MatchResult> rtn = new DataWrapper<MatchResult>();
            if (result.ID != null)
            {
                result.Entity.Id = result.ID;
            }
            var S_entity = Db.Get<string, Match>(matchID);
            rtn.Entity = S_entity.Result;
            return rtn;
        }

        #endregion
    }
}