using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;
using System.Diagnostics;

namespace CloudDeploy.Model.Releases
{
    public class DeploymentUnit
    {
        public Guid DeploymentUnitID { get; set; }
        public Build Build { get; set; }
        public DeployableArtefact DeployableArtefact { get; set; }
        public List<Host> DeployedHosts { get; set; }
        public ReleaseStatus Status { get; set; }

        public DeploymentUnit(Build build, DeployableArtefact deployableArtefact)
        {
            if (build == null) throw new ArgumentNullException("build must not be null");
            if (deployableArtefact == null) throw new ArgumentNullException("deployableArtefact must not be null");
            Build = build;
            DeployableArtefact = deployableArtefact;
            DeployedHosts = new List<Host>();
        }


        public void DeployToHost(Host host)
        {
            Trace.Write("Deploying to host:" + host.HostName);
            DeployedHosts.Add(host);
            this.Status = ReleaseStatus.InProgress;
        }


    }
}
