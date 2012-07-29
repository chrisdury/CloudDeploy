using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Platform
{
    public class Host
    {
        public string Environment { get; set; }

        public Guid DeploymentTargetID { get; set; }

        public string HostName { get; set; }

        public string HostRole { get; set; }

        public Host()
        {


        }

    }
}
