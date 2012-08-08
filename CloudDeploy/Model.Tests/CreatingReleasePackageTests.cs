using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Model.Tests.Initial;
using CloudDeploy.Model.Releases;
using System.Diagnostics;

namespace CloudDeploy.Model.Tests
{
    [TestClass]
    public class CreatingReleasePackageTests : BaseModelTest
    {
        [TestMethod]
        public void ModelTests_CreatingReleasePackage_InitialStub()
        {
            PopulateTestData();

            // create some release packages
            var rp_test = new ReleasePackage("Test Release 1", DateTime.Now.AddMinutes(20), "TEST");
            //var rp_staging = new ReleasePackage("Staging Release 1", DateTime.Now.AddMinutes(20), "STAGE");
            
            // need to have deployment units
            DeployableArtefacts
                //.Where(da => da.HostRole == "SQL Server")
                .ToList()
                .ForEach(da =>
                {
                    //rp_test.AddDeploymentUnit(new DeploymentUnit(Builds.First(), da));
                    //rp_staging.AddDeploymentUnit(new DeploymentUnit(Builds[2], da));
                });


            // show the pending deployment package
            Debug.WriteLine(rp_test.ToString()); 

            // deploy to some hosts (we will target them all, but use the environment filter)           
            rp_test.DeployToHosts(Hosts);

            Debug.WriteLine(rp_test.ToString());

            
        }
    }
}
