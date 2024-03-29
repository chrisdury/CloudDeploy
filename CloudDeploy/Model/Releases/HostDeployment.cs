﻿using System;
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
        public DateTime LastActivityDate { get; set; }
        public int Status_Id
        {
            get { return (int)ReleaseStatus; }
            set { ReleaseStatus = (ReleaseStatus)value; }
        }

        public HostDeployment() { ReleaseStatus = ReleaseStatus.Queued; LastActivityDate = DateTime.Now; }

        public void Accept()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.Queued) throw new InvalidOperationException("ReleaseStatus must be queued in order to accept");
            ReleaseStatus = Releases.ReleaseStatus.Pending;
            LastActivityDate = DateTime.Now;
        }

        public void Install()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.Pending) throw new InvalidOperationException("ReleaseStatus must be pending in order to install");
            ReleaseStatus = Releases.ReleaseStatus.InProgress;
            LastActivityDate = DateTime.Now;
        }

        public void Confirm()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.InProgress) throw new InvalidOperationException("ReleaseStatus must be InProgress to confirm install");
            ReleaseStatus = Releases.ReleaseStatus.Complete;
            LastActivityDate = DateTime.Now;
        }

        public void RollBack()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.InProgress) throw new InvalidOperationException("Release Status must be InProgress in order to rollback");
            ReleaseStatus = Releases.ReleaseStatus.Rollingback;
            LastActivityDate = DateTime.Now;
        }

        public void Fail()
        {
            if (ReleaseStatus != Releases.ReleaseStatus.Rollingback) throw new InvalidOperationException("ReleaseStatus must be Rollback to fail");
            ReleaseStatus = Releases.ReleaseStatus.Failed;
            LastActivityDate = DateTime.Now;
        }



    }
}
