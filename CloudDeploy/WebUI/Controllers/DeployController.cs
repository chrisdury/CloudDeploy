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


    }
}
