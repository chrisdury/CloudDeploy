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
    public class BuildController : Controller
    {
        private ReleaseContext db = new ReleaseContext();

        //
        // GET: /Build/

        public ActionResult Index()
        {
            return View(db.Builds.ToList());
        }

        //
        // GET: /Build/Details/5

        public ActionResult Details(Guid id)
        {
            Build build = db.Builds.Find(id);
            if (build == null)
            {
                return HttpNotFound();
            }
            return View(build);
        }

        //
        // GET: /Build/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Build/Create

        [HttpPost]
        public ActionResult Create(Build build)
        {
            if (ModelState.IsValid)
            {
                build.BuildID = Guid.NewGuid();
                db.Builds.Add(build);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(build);
        }

        //
        // GET: /Build/Edit/5

        public ActionResult Edit(Guid id)
        {
            Build build = db.Builds.Find(id);
            if (build == null)
            {
                return HttpNotFound();
            }
            return View(build);
        }

        //
        // POST: /Build/Edit/5

        [HttpPost]
        public ActionResult Edit(Build build)
        {
            if (ModelState.IsValid)
            {
                db.Entry(build).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(build);
        }

        //
        // GET: /Build/Delete/5

        public ActionResult Delete(Guid id)
        {
            Build build = db.Builds.Find(id);
            if (build == null)
            {
                return HttpNotFound();
            }
            return View(build);
        }

        //
        // POST: /Build/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Build build = db.Builds.Find(id);
            db.Builds.Remove(build);
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