using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Persistence.Contexts;

namespace CloudDeploy.Persistence.Tests
{
    [TestClass]
    public class PopuldateTestData
    {
        [TestMethod]
        public void PopulateData()
        {
            var rc = new ReleaseContext();

            var mt = new Model.Tests.Initial.BaseModelTest();
            mt.PopulateTestData();
            mt.Builds.ForEach(b => rc.Builds.Add(b));

            mt.DeployableArtefacts.ForEach(da =>
                {
                    rc.DeployableArtefacts.Add(da);
                });

            mt.Hosts.ForEach(h => rc.Hosts.Add(h));

            rc.SaveChanges();



        }
    }
}
