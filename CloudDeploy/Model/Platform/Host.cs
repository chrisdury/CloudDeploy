using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Platform
{
    public class Host
    {
        public string Environment { get; set; }

        public Guid HostID { get; set; }

        public string HostName { get; set; }

        public string HostRole { get; set; }

        public Host()
        {


        }

        public override string ToString()
        {
            return String.Format("ID: {0} HostName: {1} Role: {2} Environment: {3}", HostID, HostName, HostRole, Environment);
        }

    }
}
