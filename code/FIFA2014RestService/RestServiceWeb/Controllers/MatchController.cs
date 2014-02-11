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
    public class MatchController : ApiController
    {
        #region common CRUD implements
        // GET api/<controller>
        public IEnumerable<DataWrapper<Match>> Get()
        {
            var rtn = new List<DataWrapper<Match>>();

            return rtn;
        }

        // GET api/<controller>/5
        public DataWrapper<Match> Get(int id)
        {
            var rtn = new DataWrapper<Match>();

            return rtn;
        }

        // POST api/<controller>
        public void Post([FromBody]Match value)
        {

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Match value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }

        #endregion

        #region logic relation BLL
        [Route("api/match/{matchID}/result")]
        public DataWrapper<MatchResult> GetResultOfMatch(string matchID)
        {
            var rtn = new DataWrapper<MatchResult>();

            return rtn;
        }

        #endregion
    }
}