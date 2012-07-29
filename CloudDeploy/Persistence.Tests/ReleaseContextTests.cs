using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Persistence.Contexts;

namespace CloudDeploy.Persistence.Tests
{
    [TestClass]
    public class ReleaseContextTests
    {
        [TestMethod]
        public void ReleaseContextTests_CreateInstance()
        {
            var rc = new ReleaseContext();

            Assert.IsNotNull(rc, "ReleaseContext should not be null");
        }

        [TestMethod]
        public void ReleaseContextTests_CanAccessData()
        {
            var rc = new ReleaseContext();

            var query = from dt in rc.DeploymentTargets
                        select dt;

            Assert.IsTrue(query.Count() >= 0);

        }
    }
}
