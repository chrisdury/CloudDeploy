using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Releases
{
    public class DeploymentTarget
    {
        public PlatformEnvironment Environment { get; set; }

        public Guid DeploymentTargetID { get; set; }

        public string HostName { get; set; }

        public List<HostRole> HostRoles { get; set; }

        public DeploymentTarget()
        {
            HostRoles = new List<HostRole>();

        }

    }
}
