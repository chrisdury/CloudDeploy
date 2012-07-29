using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Model.Tests.Initial;
using CloudDeploy.Model.Releases;

namespace CloudDeploy.Model.Tests
{
    [TestClass]
    public class CreatingReleasePackageTests : BaseModelTest
    {
        [TestMethod]
        public void ModelTests_CreatingReleasePackage_InitialStub()
        {
            PopulateTestData();

            // create a release package
            var rp = new ReleasePackage("Test Release 1", DateTime.Now.AddMinutes(20), "TEST");
            // need to have deployment units


            //var du1 = new DeploymentUnit(Builds.First(), DeployableArtefacts[0]);
            var du2 = new DeploymentUnit(Builds.First(), DeployableArtefacts[1]);
            //var du3 = new DeploymentUnit(Builds.First(), DeployableArtefacts[2]);
            //rp.AddDeploymentUnit(du1);
            rp.AddDeploymentUnit(du2);
            //rp.AddDeploymentUnit(du3);


            rp.Deploy(Hosts);

            Assert.IsTrue(rp.HostReleaseRecords.Count() > 0);




        }

    }
}
