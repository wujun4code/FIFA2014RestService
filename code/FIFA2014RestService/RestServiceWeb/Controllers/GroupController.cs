using BaaSReponsitory;
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
    public class GroupController : BaseApiController<string, Group>
    {
        IGroup db = new SimpleGroupService();

        #region logic relation BLL

        [Route("api/group/{groupID}/teams")]
        public IEnumerable<DataWrapper<Team>> GetAllTeamsInCurrentGroup(string groupID)
        {
            return this.GetRelated<Group, Team>(groupID, "Teams");
        }
        [Route("api/group/{groupID}/teams")]
        [HttpPost]
        public DataWrapper<Team> AddNewTeamToGroup(string groupID, Team newTeam)
        {
            var rtn = new DataWrapper<Team>();
            var targetGroup = db.Get(groupID);
            targetGroup.Teams = new List<Team>();
            targetGroup.Teams.Add(newTeam);
            rtn.Entity = newTeam;
            db.Update(targetGroup);
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
