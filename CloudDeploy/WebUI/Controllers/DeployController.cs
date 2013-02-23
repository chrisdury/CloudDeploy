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
            return View();
        }


        public ActionResult CreateRelease()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRelease(ReleasePackage releasePackage)
        {
            if (ModelState.IsValid)
            {
                if (releasePackage.ReleasePackageID == Guid.Empty)
                    releasePackage.ReleasePackageID = Guid.NewGuid();
                releasePackage.PlatformEnvironment = "TEST";
                releasePackage.ReleaseDate = DateTime.Now;
                releasePackage.ReleaseStatus = ReleaseStatus.Queued;
                db.ReleasePackages.Add(releasePackage);                
                db.SaveChanges();
                return RedirectToAction("EditRelease");

            }
            return View(releasePackage);
        }



        public ActionResult EditRelease()
        {
            return View();
        }

       




    }
}
