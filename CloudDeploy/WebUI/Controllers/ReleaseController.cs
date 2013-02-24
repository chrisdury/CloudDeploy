using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudDeploy.Model.Releases;
using CloudDeploy.Persistence.Contexts;
using CloudDeploy.Model.Platform;

namespace WebUI.Controllers
{
    public class ReleaseController : Controller
    {
        private ReleaseContext db = new ReleaseContext();
        const string ViewDataKeys_Artefacts = "Artefacts";
        const string ViewDataKeys_Builds = "Builds";


        public ActionResult ManageDeploymentUnits(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            return View(releasepackage);
        }

        public ActionResult AddDeploymentUnit(Guid id)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);
            ViewData.Add(ViewDataKeys_Artefacts, db.GetDeployableArtefacts().ToList().Select(da => new SelectListItem() { Text = da.DeployableArtefactName, Value = da.DeployableArtefactID.ToString() }).AsEnumerable<SelectListItem>());
            ViewData.Add(ViewDataKeys_Builds, db.GetBuilds().ToList().Select(b => new SelectListItem() { Text = b.BuildName, Value = b.BuildLabel }).AsEnumerable<SelectListItem>());

            return View(releasepackage);
        }

        [HttpPost]
        public ActionResult AddDeploymentUnit(Guid id, FormCollection fc)
        {
            ReleasePackage releasepackage = db.ReleasePackages.Find(id);

            if (fc[ViewDataKeys_Artefacts] != null && fc[ViewDataKeys_Artefacts].Length > 0 && fc[ViewDataKeys_Builds] != null && fc[ViewDataKeys_Builds].Length > 0)
            {
                var artefactsIDs = fc[ViewDataKeys_Artefacts].Split(',');
                var build = db.GetBuildByLabel(fc[ViewDataKeys_Builds]);
                foreach (var artefactID in artefactsIDs)
                {
                    var artefaceGuid = new Guid(artefactID);
                    releasepackage.AddArtefactToPackage(
                        db.GetDeployableArtefacts().Single(da => da.DeployableArtefactID == artefaceGuid),
                        build);
                }
                db.SaveChanges();
                return RedirectToAction("ManageDeploymentUnits", new { id = id });
            }            
            return View();
        }

        public ActionResult RemoveDeploymentUnit(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult RemoveDeploymentUnit(Guid id, Guid deploymentUnitId)
        {
            var package = db.GetReleasePackages().Single(rp => rp.ReleasePackageID == id);
            var deploymentUnit = db.DeploymentUnits.Single(du => du.DeploymentUnitID == deploymentUnitId);
            package.RemoveArtefactFromPackage(deploymentUnit.DeployableArtefact);
            db.SaveChanges();
            return RedirectToAction("ManageDeploymentUnits", new { id = id });
            //return View();
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