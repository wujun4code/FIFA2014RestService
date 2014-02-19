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

        [Route("api/match/{matchID}/hostTeam")]
        public DataWrapper<Team> GetHostTeam(string matchID)
        {
            return this.GetMany2OneRelated<Match, Team>(matchID, "HostTeam");
        }

        [Route("api/match/{matchID}/hostTeam")]
        [HttpPost]
        public DataWrapper<Team> SetHostTeam(string matchID, DataWrapper<Team> hostTeam)
        {
            return this.AssignRelated<Match, Team>(matchID, "HostTeam", hostTeam);
        }

        [Route("api/match/{matchID}/guestTeam")]
        public DataWrapper<Team> GetGuestTeam(string matchID)
        {
            return this.GetMany2OneRelated<Match, Team>(matchID, "GuestTeam");
        }

        [Route("api/match/{matchID}/guestTeam")]
        [HttpPost]
        public DataWrapper<Team> SetGuestTeam(string matchID, DataWrapper<Team> guestTeam)
        {
            return this.AssignRelated<Match, Team>(matchID, "GuestTeam", guestTeam);
        }

        #endregion

        #region custom logic BLL
        //[Route("api/match/next/{trigger}")]
        //public DataWrapper<Match> GetResultOfMatch(int trigger)
        //{
        //    return this.GetMany2OneRelated<Match, MatchResult>(matchID);
        //}
        #endregion
    }
}