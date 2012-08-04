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
        public DbSet<Host> Hosts { get; set; }
        public ReleaseContext() : base("name=CloudDeploy.Model") { }


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

        public void AddBuild(string buildLabel)
        { }

        public void DeleteBuild(Guid buildID)
        {
        }

        public void AddReleasePackage(string releaseName, DateTime releaseDate, string environment)
        { }

        public void AddDeploymentUnitToPackage()
        { }

        public void RemoveDeploymentUnitFromPackage()
        { }

        





    }
}
