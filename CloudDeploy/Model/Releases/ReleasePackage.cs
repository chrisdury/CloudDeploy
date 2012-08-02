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

        public event DeployHander OnDeploymentUnitDeploying = delegate { };
        public event DeployHander OnDeploymentUnitDeployed = delegate { };
        public delegate void DeployHander(ReleasePackage rp, DeploymentUnit du, Host h);

        public ReleasePackage(string releaseName, DateTime releaseDate, string environment)
        {
            DeploymentUnits = new List<DeploymentUnit>();
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


        public void DeployToHosts(List<Host> hosts)
        {
            this.ReleaseStatus = Releases.ReleaseStatus.InProgress;
            foreach (var du in DeploymentUnits)
            {
                foreach (var host in hosts.Where(h => h.Environment == PlatformEnvironment))
                {
                    if (host.HostRole.Contains(du.DeployableArtefact.HostRole) || du.DeployableArtefact.HostRole == "ALL")
                    {
                        OnDeploymentUnitDeploying(this, du, host);
                        du.DeployToHost(host);
                        //OnDeploymentUnitDeployed(this, du, host); // we can't know that it has been deployed
                    }
                }
            }
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("Release Package: {0} {1} ", this.ReleaseName, this.ReleaseDate));

            if (DeploymentUnits.Count() > 0)
            {
                var rowtemplate = "|{0,-50}|{1,-10}|{2,-15}|{3,-15}|{4,-25}|";
                sb.AppendLine(String.Format(rowtemplate, "Artefact", "Build", "Env", "HostName", "Role"));

                foreach (var deploymentUnit in DeploymentUnits)
                {
                    foreach (var host in deploymentUnit.DeployedHosts)
                    {
                        sb.AppendLine(String.Format(rowtemplate,
                            deploymentUnit.DeployableArtefact.DeployableArtefactName,
                            deploymentUnit.Build.BuildLabel,
                            host.Environment,
                            host.HostName,
                            host.HostRole
                            )
                        );
                    }
                }
            }
            else
            {
                sb.AppendLine("This package has no deployment units");
            }
            return sb.ToString();
        }
    }
}
