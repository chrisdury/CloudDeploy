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
        public virtual Build Build { get; set; }
        public virtual DeployableArtefact DeployableArtefact { get; set; }
        public virtual List<HostDeployment> HostDeployments { get; set; }
        public ReleaseStatus ReleaseStatus { get; set; }
        public int Status_Id
        {
            get { return (int)ReleaseStatus; }
            set { ReleaseStatus = (ReleaseStatus)value; }
        }

        public DeploymentUnit() { HostDeployments = new List<HostDeployment>(); }

        public DeploymentUnit(Build build, DeployableArtefact deployableArtefact)
        {
            if (build == null) throw new ArgumentNullException("build must not be null");
            if (deployableArtefact == null) throw new ArgumentNullException("deployableArtefact must not be null");
            Build = build;
            DeployableArtefact = deployableArtefact;
            HostDeployments = new List<HostDeployment>();
            ReleaseStatus = ReleaseStatus.Pending;
            DeploymentUnitID = Guid.NewGuid();
            Trace.WriteLine("Created new " + this.ToString());
        }


        public void DeployToHost(Host host)
        {
            if (host == null) throw new ArgumentNullException("host", "host must not be null");
            Trace.WriteLine("Adding new Host to " + ToString());
            HostDeployments.Add(new HostDeployment() { HostDeploymentId = Guid.NewGuid(), DeploymentUnit = this, Host = host });
            ReleaseStatus = ReleaseStatus.InProgress;
        }


        public void Install()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.Pending) throw new InvalidOperationException("ReleaseStatus must be pending in order to install");
            ReleaseStatus = Releases.ReleaseStatus.InProgress;
        }

        public void Confirm()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.InProgress) throw new InvalidOperationException("ReleaseStatus must be InProgress to confirm install");
            ReleaseStatus = Releases.ReleaseStatus.Complete;
        }

        public void RollBack()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.InProgress) throw new InvalidOperationException("Release Status must be InProgress in order to rollback");
            ReleaseStatus = Releases.ReleaseStatus.Rollingback;
        }

        public void Fail()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.Rollingback) throw new InvalidOperationException("ReleaseStatus must be Rollback to fail");
            ReleaseStatus = Releases.ReleaseStatus.Failed;
        }

        public override string ToString()
        {
            return String.Format("DeploymentUnit: id:{0} status:{1} artefact:{2} build:{3}", DeploymentUnitID, ReleaseStatus, DeployableArtefact.DeployableArtefactName, Build.BuildLabel);
        }


    }
}
