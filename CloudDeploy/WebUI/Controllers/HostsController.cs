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
    public class HostsController : Controller
    {
        private ReleaseContext db = new ReleaseContext();

        //
        // GET: /Hosts/

        public ActionResult Index()
        {
            return View(db.Hosts.ToList());
        }

        //
        // GET: /Hosts/Details/5

        public ActionResult Details(Guid id)
        {
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        //
        // GET: /Hosts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Hosts/Create

        [HttpPost]
        public ActionResult Create(Host host)
        {
            if (ModelState.IsValid)
            {
                host.HostID = Guid.NewGuid();
                db.Hosts.Add(host);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(host);
        }

        //
        // GET: /Hosts/Edit/5

        public ActionResult Edit(Guid id)
        {
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        //
        // POST: /Hosts/Edit/5

        [HttpPost]
        public ActionResult Edit(Host host)
        {
            if (ModelState.IsValid)
            {
                db.Entry(host).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(host);
        }

        //
        // GET: /Hosts/Delete/5

        public ActionResult Delete(Guid id)
        {
            Host host = db.Hosts.Find(id);
            if (host == null)
            {
                return HttpNotFound();
            }
            return View(host);
        }

        //
        // POST: /Hosts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Host host = db.Hosts.Find(id);
            db.Hosts.Remove(host);
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