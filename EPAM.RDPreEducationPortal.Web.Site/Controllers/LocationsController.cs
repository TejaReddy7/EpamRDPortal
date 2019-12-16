using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class LocationsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Master_Locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Locations master_Locations = db.Master_Locations.Find(id);
            if (master_Locations == null)
            {
                return HttpNotFound();
            }
            return View(master_Locations);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,Location")] Master_Locations master_Locations)
        {
            if (ModelState.IsValid)
            {
                db.Master_Locations.Add(master_Locations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master_Locations);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Locations master_Locations = db.Master_Locations.Find(id);
            if (master_Locations == null)
            {
                return HttpNotFound();
            }
            return View(master_Locations);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,Location")] Master_Locations master_Locations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master_Locations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master_Locations);
        }

        // GET: Recruitments/List
        public JsonResult List()
        {
            return Json(db.Master_Locations.ToList(), JsonRequestBehavior.AllowGet);
        }
        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Locations master_Locations = db.Master_Locations.Find(id);
            if (master_Locations == null)
            {
                return HttpNotFound();
            }
            return View(master_Locations);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master_Locations master_Locations = db.Master_Locations.Find(id);
            db.Master_Locations.Remove(master_Locations);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
