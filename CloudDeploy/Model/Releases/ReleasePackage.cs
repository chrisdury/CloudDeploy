using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;
using System.Threading.Tasks;

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

        public event DeployHander OnDeploymentUnitDeploying = delegate {};
        public event DeployHander OnDeploymentUnitDeployed = delegate { };
        public delegate void DeployHander(ReleasePackage rp, DeploymentUnit du, Host h);
        


        public ReleasePackage(string releaseName, DateTime releaseDate, string environment)
        {
            DeploymentUnits = new List<DeploymentUnit>();
            HostReleaseRecords = new List<HostReleaseRecord>();
            ReleaseName = releaseName;
            ReleaseDate = releaseDate;
            PlatformEnvironment = environment;
            ReleaseStatus = Releases.ReleaseStatus.Pending;

            OnDeploymentUnitDeployed += new DeployHander(ReleasePackage_OnDeploymentUnitDeployed);

        }

        void ReleasePackage_OnDeploymentUnitDeployed(ReleasePackage rp, DeploymentUnit du, Host h)
        {
            HostReleaseRecords.Add(new HostReleaseRecord() { DeploymentUnit = du, Host = h, HostReleaseRecordId = Guid.NewGuid() });
        }


        public void AddDeploymentUnit(DeploymentUnit deploymentUnit)
        {
            if (deploymentUnit == null) throw new ArgumentNullException("deploymentUnit must not be null");
            DeploymentUnits.Add(deploymentUnit);
        }


        public void DeployToHosts(List<Host> hosts)
        {
            foreach (var du in DeploymentUnits)
            {
                foreach (var h in hosts.Where(h => h.Environment == PlatformEnvironment))
                {
                    if (h.HostRole.Contains(du.DeployableArtefact.HostRole) || du.DeployableArtefact.HostRole == "ALL")
                    {
                        OnDeploymentUnitDeploying(this, du, h);
                        du.Deploy();
                        OnDeploymentUnitDeployed(this, du, h);
                    }
                }
            }
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            if (HostReleaseRecords.Count() > 0)
            {
                var rowtemplate = "|{0,-15}|{1,-15}|{2,-30}|{3,-50}|{4,-10}|";
                sb.AppendLine(String.Format(rowtemplate, "Env", "HostName", "Role", "Artefact", "Build"));
                foreach (var hrr in HostReleaseRecords)
                {
                    sb.AppendLine(String.Format(rowtemplate,
                        hrr.Host.Environment,
                        hrr.Host.HostName,
                        hrr.Host.HostRole,
                        hrr.DeploymentUnit.DeployableArtefact.DeployableArtefactName,
                        hrr.DeploymentUnit.Build.BuildLabel));
                }
            }
            else
            {
                sb.AppendLine("This package has not been deployed");
            }
            return sb.ToString();            
        }



    }
}
