using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebUI.Controllers
{
    public class Agent2Controller : ApiController
    {
        // GET api/agent2
        public IEnumerable<string> Get()
        {
            return new string[] { "No action here." };
        }

        // GET api/agent2/5

        public string Get(string value)
        {
            return value;
        }

        // POST api/agent2
        public void Post([FromBody]string value)
        {
        }

        // PUT api/agent2/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/agent2/5
        public void Delete(int id)
        {
        }
    }
}
