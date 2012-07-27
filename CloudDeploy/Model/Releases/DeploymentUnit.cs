using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Platform;

namespace CloudDeploy.Model.Releases
{
    public class DeploymentUnit
    {
        public Guid DeploymentUnitID { get; set; }
        public Build Build { get; set; }
        public DeployableArtefact DeployableArtefact { get; set; }
       
    }
}
