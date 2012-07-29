using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Releases
{
    public class ReleasePackage
    {
        public Guid ReleasePackageID { get; set; }
        public string ReleaseName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string PlatformEnvironment { get; set; }
        public ReleaseStatus ReleaseStatus { get; set; }
        public List<DeploymentUnit> DeploymentUnits { get; set; }
        public List<HostReleaseRecord> HostReleaseRecords { get; set; }

        public ReleasePackage(string releaseName, DateTime releaseDate, string environment)
        {
            DeploymentUnits = new List<DeploymentUnit>();
            HostReleaseRecords = new List<HostReleaseRecord>();
            ReleaseName = releaseName;
            ReleaseDate = releaseDate;
            PlatformEnvironment = environment;
            ReleaseStatus = Releases.ReleaseStatus.Pending;
        }


        public void AddDeploymentUnit(DeploymentUnit deploymentUnit)
        {
            if (deploymentUnit == null) throw new ArgumentNullException("deploymentUnit must not be null");
            DeploymentUnits.Add(deploymentUnit);
        }


        public void Deploy(List<Host> hosts)
        {
            foreach (var dp in DeploymentUnits)
            {
                foreach (var h in hosts.Where(h => h.Environment == PlatformEnvironment))
                {
                    if (h.HostRole.Contains(dp.DeployableArtefact.HostRole) || dp.DeployableArtefact.HostRole == "ALL")
                    {
                        HostReleaseRecords.Add(new HostReleaseRecord() { DeploymentUnit = dp, Host = h, HostReleaseRecordId = Guid.NewGuid() });
                    }
                }
            }
        }



    }
}
