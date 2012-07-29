using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Platform
{
    public class HostRole
    {
        public Guid HostRoleId { get; set; }
        public string Name { get; set; }

        public HostRole(string name)
        {
            Name = name;
        }
    }
}
