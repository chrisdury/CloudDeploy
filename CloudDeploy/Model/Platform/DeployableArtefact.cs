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
        public string HostRole { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0} Name: {1} Filename: {2} Host: {3}", DeployableArtefactID, DeployableArtefactName, FileName, HostRole);
        }

    }
}
