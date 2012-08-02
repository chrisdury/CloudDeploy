using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Releases
{
    public class HostDeployment
    {
        public Guid HostDeploymentId { get; set; }
        public Host Host { get; set; }
        public DeploymentUnit DeploymentUnit { get; set; }
        public ReleaseStatus Status { get; set; }

        public HostDeployment() { Status = ReleaseStatus.Pending; }
    }
}
