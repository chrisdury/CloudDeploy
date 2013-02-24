using CloudDeploy.Model.Releases;
using CloudDeploy.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class DeployController : Controller
    {
        //
        // GET: /Deploy/

        private ReleaseContext db = new ReleaseContext();

        public ActionResult Index()
        {
            ViewData.Add("Releases", db.GetReleasePackages());

            ViewData.Add("HostDeployments", db.HostDeployments.Where(hd => hd.ReleaseStatus == ReleaseStatus.Complete).OrderBy(hd => hd.DeploymentUnit.DeployableArtefact.DeployableArtefactName));


            return View();
        }


        public ActionResult DeployReleasePackage(Guid id)
        {
            var releasePackage = db.GetReleasePackages().Single(rp => rp.ReleasePackageID == id);
            ViewData.Add("Environments", new List<SelectListItem>(new SelectListItem[] { new SelectListItem() { Text = "TEST" }, new SelectListItem() { Text = "STAGING" } }));

            return View(releasePackage);
        }

        [HttpPost]
        public ActionResult DeployReleasePackage(Guid id, FormCollection fc)
        {
            var releasePackage = db.GetReleasePackages().Single(rp => rp.ReleasePackageID == id);
            if (fc["Environments"] != null)
            {
                db.DeployPackageToEnvironment(releasePackage.ReleaseName, fc["Environments"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releasePackage);
        }

        public ActionResult StatusOfReleasePackage(Guid id)
        {
            var releasePackage = db.GetReleasePackages().Single(rp => rp.ReleasePackageID == id);
            return View(releasePackage);
        }


        [HttpPost]
        public ActionResult StatusOfReleasePackage(Guid id, FormCollection fc)
        {
            var releasePackage = db.GetReleasePackages().Single(rp => rp.ReleasePackageID == id);
            var action = fc["Action"];
            if (fc["HostDeployment"] != null)
            {
                var hostDeploymentIDs = fc["HostDeployment"].Split(',');
                foreach (var hostDeploymentID in hostDeploymentIDs)
                {
                    if (hostDeploymentID.Length != 36) continue; //skip non-guid values
                    var hostDeployment = db.HostDeployments.Find(new Guid(hostDeploymentID));

                    switch (action)
                    {
                        case "Accept":
                            hostDeployment.Accept();
                            break;
                        case "Install":
                            hostDeployment.Install();
                            break;
                        case "Confirm":
                            hostDeployment.Confirm();
                            break;
                        case "Rollback":
                            hostDeployment.RollBack();
                            break;
                        case "Failed":
                            hostDeployment.Fail();
                            break;
                    }                    
                }
                db.SaveChanges();
                return RedirectToAction("StatusOfReleasePackage", new { id = id });
            }
            return View(releasePackage);
        }




    }
}
