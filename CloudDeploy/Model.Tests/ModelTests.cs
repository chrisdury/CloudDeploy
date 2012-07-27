using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Model.Platform;
using CloudDeploy.Model.Releases;

namespace Model.Tests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var da_config = new DeployableArtefact() { FileName = "WBRT.Configuration.msi", DeployableArtefactName = "Configuration", HostRole = HostRole.All };
            var da_db1 = new DeployableArtefact() { FileName = "WBRT.ProgramData.Lifecycle.DB.msi", DeployableArtefactName = "Lifecycle DB", HostRole = HostRole.SQLServer };

            var build = new Build() { BuildDate = DateTime.Now, BuildLabel = "1.0.500.0" };

            var du_config = new DeploymentUnit() { Build = build, DeployableArtefact = da_config };
            var du_db1 = new DeploymentUnit() { Build = build, DeployableArtefact = da_db1 };


            var dt1 = new DeploymentTarget() { Environment = PlatformEnvironment.Test, HostName = "TESTWBRTDB", HostRoles = new HostRole[] { HostRole.SQLServer } };

            var rp = new ReleasePackage();
            rp.DeploymentTargets.Add(dt1);
            rp.DeploymentUnits.Add(du_config);
            rp.DeploymentUnits.Add(du_db1);

            rp.ReleaseStatus = ReleaseStatus.Pending;



            Assert.IsTrue(rp != null);

        }
    }
}
