using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Platform
{
    public class DeployableArtefact
    {
        public Guid DeployableArtefactID { get; set; }
        public string DeployableArtefactName { get; set; }
        public string FileName { get; set; }
        public string InstallationScript { get; set; }
        public string RollbackScript { get; set; }
        public HostRole HostRole { get; set; }
    }
}
