using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Releases
{
    public class HostReleaseRecord
    {
        public Guid HostReleaseRecordId { get; set; }
        public Host Host { get; set; }
        public DeploymentUnit DeploymentUnit { get; set; }
    }
}
