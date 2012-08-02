using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudDeploy.Model.Releases;

namespace CloudDeploy.Model.Controller
{
    public class Controller
    {
        public List<ReleasePackage> ReleasePackages { get; set; }
        public List<Agent.Agent> Agents { get; set; }
        
        
        public void ScanForPendingJobs()
        {
            // dispatch jobs to agents?
            // seems weird to put this here.. should we only have business rules in the Model? Perhaps this is in a Service or other type

        }


    }
}
