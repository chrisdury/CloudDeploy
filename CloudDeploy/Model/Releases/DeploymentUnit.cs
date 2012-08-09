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

        public DeploymentUnit() { }

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


        public override string ToString()
        {
            return String.Format("DeploymentUnit: id:{0} status:{1} artefact:{2} build:{3}", DeploymentUnitID, ReleaseStatus, DeployableArtefact.DeployableArtefactName, Build.BuildLabel);
        }


    }
}
