﻿using RestServiceWeb.Models.DataContracts;
using RestServiceWeb.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestServiceWeb.Controllers
{
    public class GroupController : ApiController
    {

        #region common CRUD implements
        // GET api/values
        public IEnumerable<DataWrapper<Match>> Get()
        {
            var rtn = new List<DataWrapper<Match>>();

            return rtn;
        }

        // GET api/values/5
        public DataWrapper<Match> Get(int id)
        {
            var rtn = new DataWrapper<Match>();

            return rtn;
        }

        // POST api/values
        public void Post([FromBody]Match value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Match value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
        #endregion

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