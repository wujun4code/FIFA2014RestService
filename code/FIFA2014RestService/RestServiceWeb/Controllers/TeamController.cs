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
    public class TeamController : ApiController
    {

        #region common CRUD implements
        // GET api/<controller>
        public IEnumerable<DataWrapper<Team>> Get()
        {
            var rtn = new List<DataWrapper<Team>>();

            return rtn;
        }

        // GET api/<controller>/5
        public DataWrapper<Team> Get(int id)
        {
            var rtn = new DataWrapper<Team>();

            return rtn;
        }

        // POST api/<controller>
        public void Post([FromBody]Team value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Team value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
        #endregion

        
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