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
        public int Status_Id 
        {
            get { return (int)ReleaseStatus; }
            set { ReleaseStatus = (ReleaseStatus)value; }
        }
        public virtual List<DeploymentUnit> DeploymentUnits { get; set; }

        public event DeployHander OnDeploymentUnitDeploying = delegate { };
        public event DeployHander OnDeploymentUnitDeployed = delegate { };
        public delegate void DeployHander(ReleasePackage rp, DeploymentUnit du, Host h);

        public ReleasePackage() { }

        public ReleasePackage(string releaseName, DateTime releaseDate, string environment)
        {
            DeploymentUnits = new List<DeploymentUnit>();
            ReleasePackageID = Guid.NewGuid();
            ReleaseName = releaseName;
            ReleaseDate = releaseDate;
            PlatformEnvironment = environment;
            ReleaseStatus = Releases.ReleaseStatus.Pending;
        }



        public DeploymentUnit AddArtefactToPackage(DeployableArtefact artefact, Build build)
        {
            if (DeploymentUnits.Any(du => du.DeployableArtefact == artefact && du.Build == build)) throw new ArgumentException(String.Format("The artefact:{0} build:{1} has already been added", artefact.DeployableArtefactName, build.BuildLabel));

            var deploymentUnit = new DeploymentUnit(build, artefact);
            DeploymentUnits.Add(deploymentUnit);
            return deploymentUnit;
        }

        public void RemoveArtefactFromPackage(DeployableArtefact artefact)
        {
            DeploymentUnits.RemoveAll(du => du.DeployableArtefact == artefact);
        }



        public void DeployToHosts(List<Host> hosts)
        {
            this.ReleaseStatus = Releases.ReleaseStatus.InProgress;
            foreach (var du in DeploymentUnits)
            {
                if (!hosts.Any(h => h.Environment.Equals(PlatformEnvironment, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new ArgumentException("The Hosts you are intending to deploy to are not valid for this package");
                }
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
            sb.AppendLine(String.Format("Release Package: {0} on: {1} status: {2}", this.ReleaseName, this.ReleaseDate, this.ReleaseStatus));

            if (DeploymentUnits != null && DeploymentUnits.Count() > 0)
            {
                var rowtemplate = "|{0,-50}|{1,-10}|{2,-15}|{3,-15}|{4,-25}|{5,-10}|";
                sb.AppendLine(String.Format(rowtemplate, "Artefact", "Build", "Environment", "HostName", "Role", "Status"));

                foreach (var deploymentUnit in DeploymentUnits)
                {
                    sb.AppendLine(String.Format(rowtemplate,
                            deploymentUnit.DeployableArtefact.DeployableArtefactName,
                            deploymentUnit.Build.BuildLabel,
                            "",
                            "",
                            "",
                            deploymentUnit.ReleaseStatus
                            )
                        );
                    foreach (var hostDeployment in deploymentUnit.HostDeployments)
                    {
                        sb.AppendLine(String.Format(rowtemplate,
                            "",
                            "",
                            hostDeployment.Host.Environment,
                            hostDeployment.Host.HostName,
                            hostDeployment.Host.HostRole,
                            hostDeployment.ReleaseStatus
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
