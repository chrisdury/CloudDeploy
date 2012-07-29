using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CloudDeploy.Model.Releases;

namespace CloudDeploy.Persistence.Contexts
{
    public class ReleaseContext : DbContext
    {
        public DbSet<Build> Builds { get; set; }
        public DbSet<ReleasePackage> ReleasePackages { get; set; }
        public DbSet<DeploymentTarget> DeploymentTarges { get; set; }
        public DbSet<DeploymentUnit> DeploymentUnits { get; set; }

        public ReleaseContext() : base("name=ReleaseContext") { }

    }
}
