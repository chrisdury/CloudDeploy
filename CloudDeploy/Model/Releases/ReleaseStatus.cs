using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Releases
{
    public enum ReleaseStatus
    {
        Queued = 1,
        Pending,
        InProgress,
        Complete,
        Failed,
        Rollingback,

    }
}
