﻿using BaaSReponsitory;
using RestService.Core;
using RestServiceWeb.BLL.ServiceContracts;
using RestServiceWeb.BLL.ServiceImpls;
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
            return this.GetOne2ManyRelated<Group, Team>(groupID, "Teams");
        }

        [Route("api/group/{groupID}/teams")]
        [HttpPost]
        public DataWrapper<Team> AssignTeamToGroup(string groupID, DataWrapper<Team> newTeam)
        {
            return this.AssignRelated<Group, Team>(groupID, "Teams", newTeam);
        }


        [Route("api/group/{groupID}/matches")]
        public IEnumerable<DataWrapper<Match>> GetAllMatchesInCurrentGroup(string groupID)
        {
            return this.GetOne2ManyRelated<Group, Match>(groupID, "Matches");
        }

        #endregion

    }
}
