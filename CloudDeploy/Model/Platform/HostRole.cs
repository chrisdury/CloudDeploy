using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Platform
{
    public enum HostRole
    {
        All = 1,
        ActiveDirectory,
        SQLServer,
        SharePoint,
        CRM
    }
}
