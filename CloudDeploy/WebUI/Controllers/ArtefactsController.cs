using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudDeploy.Model.Platform;
using CloudDeploy.Persistence.Contexts;

namespace WebUI.Controllers
{
    public class ArtefactsController : Controller
    {
        private ReleaseContext db = new ReleaseContext();

        //
        // GET: /Artefacts/

        public ActionResult Index()
        {
            return View(db.DeployableArtefacts.ToList());
        }

        //
        // GET: /Artefacts/Details/5

        public ActionResult Details(Guid id)
        {
            DeployableArtefact deployableartefact = db.DeployableArtefacts.Find(id);
            if (deployableartefact == null)
            {
                return HttpNotFound();
            }
            return View(deployableartefact);
        }

        //
        // GET: /Artefacts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Artefacts/Create

        [HttpPost]
        public ActionResult Create(DeployableArtefact deployableartefact)
        {
            if (ModelState.IsValid)
            {
                deployableartefact.DeployableArtefactID = Guid.NewGuid();
                db.DeployableArtefacts.Add(deployableartefact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deployableartefact);
        }

        //
        // GET: /Artefacts/Edit/5

        public ActionResult Edit(Guid id)
        {
            DeployableArtefact deployableartefact = db.DeployableArtefacts.Find(id);
            if (deployableartefact == null)
            {
                return HttpNotFound();
            }
            return View(deployableartefact);
        }

        //
        // POST: /Artefacts/Edit/5

        [HttpPost]
        public ActionResult Edit(DeployableArtefact deployableartefact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deployableartefact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deployableartefact);
        }

        //
        // GET: /Artefacts/Delete/5

        public ActionResult Delete(Guid id)
        {
            DeployableArtefact deployableartefact = db.DeployableArtefacts.Find(id);
            if (deployableartefact == null)
            {
                return HttpNotFound();
            }
            return View(deployableartefact);
        }

        //
        // POST: /Artefacts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DeployableArtefact deployableartefact = db.DeployableArtefacts.Find(id);
            db.DeployableArtefacts.Remove(deployableartefact);
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