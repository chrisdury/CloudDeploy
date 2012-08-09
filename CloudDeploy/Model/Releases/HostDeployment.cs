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
        public virtual Host Host { get; set; }
        public virtual DeploymentUnit DeploymentUnit { get; set; }
        public ReleaseStatus ReleaseStatus { get; set; }
        public int Status_Id
        {
            get { return (int)ReleaseStatus; }
            set { ReleaseStatus = (ReleaseStatus)value; }
        }

        public HostDeployment() { ReleaseStatus = ReleaseStatus.Pending; }
    }
}
