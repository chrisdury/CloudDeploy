using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CloudDeploy.Model.Releases;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Persistence.Contexts
{
    public class ReleaseContext : DbContext, IDisposable
    {
        public DbSet<Build> Builds { get; set; }
        public DbSet<ReleasePackage> ReleasePackages { get; set; }
        public DbSet<DeploymentUnit> DeploymentUnits { get; set; }
        public DbSet<DeployableArtefact> DeployableArtefacts { get; set; }
        public DbSet<HostDeployment> HostDeployments { get; set; }
        public DbSet<Host> Hosts { get; set; }
        public ReleaseContext() : base("name=CloudDeploy.Model") { }

        #region Hosts

        public void AddHost(string hostName, string hostRole, string environment)
        {
            try
            {
                if (Hosts.Any(h => h.HostName == hostName)) throw new InvalidOperationException(String.Format("Host '{0}' already exists", hostName));
                var host = new Host()
                {
                    HostID = Guid.NewGuid(),
                    HostName = hostName,
                    HostRole = hostRole,
                    Environment = environment
                };                
                Hosts.Add(host);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Problem adding Host '{0}'. " + ex.Message, hostName), ex);
            }
        }

        public IQueryable<Host> GetHosts()
        {
            return Hosts;
        }

        public IQueryable<Host> GetHostsInEnvironment(string environmentName)
        {
            return GetHosts().Where(h => h.Environment.Equals(environmentName, StringComparison.InvariantCultureIgnoreCase));
        }

        public void UpdateHost(Host host)
        {
            try
            {
                var updatingHost = Hosts.Single(h => h.HostName == host.HostName);
                updatingHost.Environment = host.Environment;
                updatingHost.HostRole = host.HostRole;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Problem updating Host '{0}'. " + ex.Message, host.HostName), ex);
            }
        }

        public void DeleteHost(string hostName)
        {
            try
            {
                var host = Hosts.Single(h => hostName == h.HostName);
                Hosts.Remove(host);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Problem deleting Host '{0}'. " + ex.Message, hostName), ex);
            }
        }

        #endregion

        #region Builds

        public void AddBuild(string buildLabel, DateTime buildDate, string dropLocation)
        {
            if (Builds.Any(b => b.BuildLabel == buildLabel)) throw new InvalidOperationException(String.Format("A build with label '{0}' already exists.", buildLabel));
            try
            {
                var build = new Build()
                {
                    BuildLabel = buildLabel,
                    BuildDate = buildDate,
                    BuildID = Guid.NewGuid(),
                    BuildName = "Build " + buildLabel + " on " + buildDate.ToString(),
                    DropLocation = dropLocation
                };
                Builds.Add(build);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not add Build. " + ex.Message, ex);
            }        
        }

        public void DeleteBuild(string buildLabel)
        {
            try
            {
                var build = Builds.Single(b => b.BuildLabel == buildLabel);
                Builds.Remove(build);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Problem deleting Build '{0}'. " + ex.Message, buildLabel), ex);
            }
        }

        public IQueryable<Build> GetBuilds()
        {
            return Builds;//.Include("DeploymentUnits");
        }

        public Build GetBuildByLabel(string label)
        {
            var build = GetBuilds().SingleOrDefault(b => b.BuildLabel.Equals(label, StringComparison.InvariantCultureIgnoreCase));
            if (build == null) throw new ArgumentOutOfRangeException("name", "Could not find build with label:" + label);
            return build;
        }


        #endregion

        #region Artefacts

        public void AddArtefact(string artefactName, string fileName, string hostRole)
        {
            if (DeployableArtefacts.Any(da => da.DeployableArtefactName == artefactName)) throw new ArgumentException("Deployable artefact already added");
            try
            {
                var deployableArtefact = new DeployableArtefact()
                {
                    DeployableArtefactID = Guid.NewGuid(),
                    DeployableArtefactName = artefactName,
                    FileName = fileName,
                    HostRole = hostRole
                };
                DeployableArtefacts.Add(deployableArtefact);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not add Deployable Artefact", ex);
            }
        }

        public void DeleteArtefact(string artefactName)
        {
            try
            {
                var deployableArtefact = DeployableArtefacts.Single(da => da.DeployableArtefactName == artefactName);
                DeployableArtefacts.Remove(deployableArtefact);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not delete Deployable Artefact", ex);
            }
        }

        public IQueryable<DeployableArtefact> GetDeployableArtefacts()
        {
            return DeployableArtefacts;
        }

        public DeployableArtefact GetArtefactByName(string name)
        {
            var artefact = GetDeployableArtefacts().SingleOrDefault(a => a.DeployableArtefactName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (artefact == null) throw new ArgumentOutOfRangeException("artefact", "Could not find an artefact with name:" + name);
            return artefact;
        }

        #endregion


        public List<HostDeployment> GetCurrentCatalogOfArtefactsByEnvironment()
        {
            var g = from hd in GetCurrentCatalogOfArtefacts()
                    group hd by hd.Host.Environment into grouping
                    select grouping;

            var hostDeployments = new List<HostDeployment>();
            foreach (var i in g)
            {
                hostDeployments.AddRange(i.ToList());
            }
            return hostDeployments;
        }


        public IQueryable<HostDeployment> GetCurrentCatalogOfArtefacts()
        {
            var deploymentUnits = from hd in HostDeployments
                                  where hd.ReleaseStatus == ReleaseStatus.Complete
                                  group hd by new { hd.DeploymentUnit.DeployableArtefact.DeployableArtefactName, hd.Host.Environment, hd.Host.HostName } into groupedHostDeployments
                                  let latestActivity = groupedHostDeployments.Max(la => la.LastActivityDate)
                                  select new
                                  {
                                      ArtefactName = groupedHostDeployments.Key.DeployableArtefactName,
                                      LastActivity = latestActivity,
                                      Environment = groupedHostDeployments.Key.Environment,
                                      HostName = groupedHostDeployments.Key.HostName,
                                      Unit = groupedHostDeployments.FirstOrDefault(
                                        ghd => ghd.DeploymentUnit.DeployableArtefact.DeployableArtefactName == groupedHostDeployments.Key.DeployableArtefactName &&
                                               //ghd.LastActivityDate == groupedHostDeployments.Key.LastActivity &&
                                               ghd.Host.Environment == groupedHostDeployments.Key.Environment &&
                                               ghd.Host.HostName == groupedHostDeployments.Key.HostName)
                                  };

            return from hdu in deploymentUnits
                   select hdu.Unit;

        }

        

        #region Release Package

        public IQueryable<ReleasePackage> GetReleasePackages()
        {
            return ReleasePackages.Include("DeploymentUnits");//.Include("DeployableArtefacts");
        }

        public ReleasePackage GetReleasePackageByName(string name)
        {
            var releasePackage = GetReleasePackages().SingleOrDefault(rp => rp.ReleaseName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (releasePackage == null) throw new ArgumentOutOfRangeException("Could not find Release Package with name:" + name);
            return releasePackage;
        }

        public ReleasePackage AddReleasePackage(string releaseName, DateTime releaseDate, string environment)
        {
            try
            {
                var rp = new ReleasePackage(releaseName, releaseDate, environment);
                ReleasePackages.Add(rp);
                SaveChanges();
                return rp;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not add Release Package", ex);
            }
        }

        public DeploymentUnit AddArtefactToPackage(string packageName, string artefactName, string buildLabel)
        {
            try
            {
                var artefact = GetArtefactByName(artefactName);
                var package = GetReleasePackageByName(packageName);
                var build = GetBuildByLabel(buildLabel);

                var deploymentUnit = package.AddArtefactToPackage(artefact, build);                
                SaveChanges();
                return deploymentUnit;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Problem adding artefact '{0}' to package '{1}'", artefactName, packageName), ex);
            }
        }

        public void RemoveArtefactFromPackage(string packageName, string artefactName)
        {
            try
            {
                var package = GetReleasePackageByName(packageName);
                var artefact = GetArtefactByName(artefactName);
                package.RemoveArtefactFromPackage(artefact);
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("There was a problem removing artefact:{0} from package:{1}. " + ex.Message, artefactName, packageName), ex);
            }
        }


        public ReleasePackage DeployPackageToEnvironment(string packageName, string environmentName)
        {
            try
            {
                var package = GetReleasePackageByName(packageName);
                var hosts = GetHostsInEnvironment(environmentName);
                if (hosts.Count() == 0) throw new ArgumentException("environmentName", "there are no hosts configured for the environment: " + environmentName);
                
                package.DeployToHosts(hosts.ToList());
                SaveChanges();
                return package;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Problem deploying package'{0}' to environment '{1}'. " + ex.Message, packageName, environmentName), ex);
            }
        }


        #endregion
    }
}
