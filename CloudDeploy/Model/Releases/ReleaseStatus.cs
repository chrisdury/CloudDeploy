using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Releases
{
    public enum ReleaseStatus
    {
        Pending = 1,
        InProgress,
        Complete,
        Failed,
        Rollingback,

    }
}
