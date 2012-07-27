using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Releases
{
    public class ReleasePackage
    {
        public List<DeploymentUnit> DeploymentUnits { get; set; }
        public List<DeploymentTarget> DeploymentTargets { get; set; }

        public string ReleaseName { get; set; }
        public Guid ReleasePackageID { get; set; }
        public DateTime ReleaseDate { get; set; }

        public ReleaseStatus ReleaseStatus { get; set; }

        public ReleasePackage()
        {
            DeploymentUnits = new List<DeploymentUnit>();
            DeploymentTargets = new List<DeploymentTarget>();
        }
    }
}
