using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;
using CloudDeploy.Model.Releases;

namespace CloudDeploy.Model.Agent
{
    public class Agent
    {
        public Host Host { get; set; }

        //public IEnumerable<DeploymentUnit> CurrentDeploymentUnits { get; set; }

        public HostDeployment CheckForWork(IQueryable<HostDeployment> hostDeployments)
        {
            return null;
        }

    }
}
