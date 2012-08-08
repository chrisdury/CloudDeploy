using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudDeploy.Model.Releases
{
    public class Build
    {
        public string BuildLabel { get; set; }
        public string DropLocation { get; set; }
        public Guid BuildID { get; set; }
        public string BuildName { get; set; }
        public DateTime BuildDate { get; set; }
        public virtual IEnumerable<DeploymentUnit> DeploymentUnits { get; set; }


        public override string ToString()
        {
            return String.Format("ID: {0} BuildName: {1} Label: {2} Date: {3}", BuildID, BuildName, BuildLabel, BuildDate);
        }

    }
}
