using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudDeploy.Model.Releases;
using CloudDeploy.Persistence.Contexts;

namespace WebUI.Controllers
{
    public class ReleaseController : Controller
    {
        private ReleaseContext db = new ReleaseContext();



        public ActionResult ManageDeploymentUnits(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);

            
            

            return View(releasepackage);
        }

        public ActionResult AddDeploymentUnit(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            ViewData.Add("Artefacts", db.GetDeployableArtefacts().ToList().Select(da => new SelectListItem() { Text = da.DeployableArtefactName, Value = da.DeployableArtefactID.ToString() }).AsEnumerable<SelectListItem>());
            return View(releasepackage);
        }

        [HttpPost]
        public ActionResult AddDeploymentUnit(FormCollection fc)
        {
            //ReleasePackage releasepackage = db.ReleasePackages.Find(id);

            

            RedirectToAction("ManageDeploymentUnits");
            return View();
        }





        //
        // GET: /Release/

        public ActionResult Index()
        {
            return View(db.ReleasePackages.ToList());
        }

        //
        // GET: /Release/Details/5

        public ActionResult Details(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            if (releasepackage == null)
            {
                return HttpNotFound();
            }
            return View(releasepackage);
        }

        //
        // GET: /Release/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Release/Create

        [HttpPost]
        public ActionResult Create(ReleasePackage releasepackage)
        {
            if (ModelState.IsValid)
            {
                releasepackage.ReleasePackageID = Guid.NewGuid();
                releasepackage.PlatformEnvironment = "TEST";
                releasepackage.ReleaseDate = DateTime.Now;
                releasepackage.ReleaseStatus = ReleaseStatus.Queued;
                db.ReleasePackages.Add(releasepackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(releasepackage);
        }

        //
        // GET: /Release/Edit/5

        public ActionResult Edit(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            if (releasepackage == null)
            {
                return HttpNotFound();
            }
            return View(releasepackage);
        }

        //
        // POST: /Release/Edit/5

        [HttpPost]
        public ActionResult Edit(ReleasePackage releasepackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releasepackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releasepackage);
        }

        //
        // GET: /Release/Delete/5

        public ActionResult Delete(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            if (releasepackage == null)
            {
                return HttpNotFound();
            }
            return View(releasepackage);
        }

        //
        // POST: /Release/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            db.ReleasePackages.Remove(releasepackage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}