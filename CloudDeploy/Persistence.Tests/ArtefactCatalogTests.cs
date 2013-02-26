using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudDeploy.Persistence.Contexts;

namespace CloudDeploy.Persistence.Tests
{
    [TestClass]
    public class ArtefactCatalogTests
    {
        [TestMethod]
        public void GetCurrentCatalogOfArtefacts_ReturnsSomething()
        {
            var rc = new ReleaseContext();

            var artefacts = rc.GetCurrentCatalogOfArtefacts();

            Assert.IsTrue(artefacts.Count() > 0);


        }
    }
}
