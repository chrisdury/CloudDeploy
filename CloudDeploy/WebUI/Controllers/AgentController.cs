using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CloudDeploy.Model.Releases;
using CloudDeploy.Persistence.Contexts;

namespace WebUI.Controllers
{
    public class AgentController : ApiController
    {
        ReleaseContext rc;
        public AgentController()
        {
            rc = new ReleaseContext();
        }

        public class AgentJobDataObject
        {
            public string HostName { get; set; }
            public string ArtefactName { get; set; }
            public string BuildNumber { get; set; }
        }


        // GET api/agent2
        public IEnumerable<string> Get()
        {
            return new string[] { "No action here." };
        }

        // GET api/agent2/5
        public int Get(int id)
        {
            return 4;
        }

        public string Get(string value)
        {
            return value;
        }

        public IEnumerable<AgentJobDataObject> GetDeploymentsForHost(string host)
        {
            //return rc.HostDeployments.Where(hd => hd.Host.HostName == host);

            return from du in rc.DeploymentUnits
                   from hd in rc.HostDeployments
                   where hd.Host.HostName == host && hd.ReleaseStatus == ReleaseStatus.Queued
                   select new AgentJobDataObject()
                   {
                       HostName = hd.Host.HostName,
                       ArtefactName = du.DeployableArtefact.DeployableArtefactName,
                       BuildNumber = du.Build.BuildLabel
                   };

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
