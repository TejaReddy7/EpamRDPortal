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
    public class GenderController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Gender
        public ActionResult Index()
        {
            return View(db.Master_Gender.ToList());
        }

        // GET: Gender/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Gender master_Gender = db.Master_Gender.Find(id);
            if (master_Gender == null)
            {
                return HttpNotFound();
            }
            return View(master_Gender);
        }

        // GET: Gender/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gender/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenderId,Gender")] Master_Gender master_Gender)
        {
            if (ModelState.IsValid)
            {
                db.Master_Gender.Add(master_Gender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master_Gender);
        }

        // GET: Gender/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Gender master_Gender = db.Master_Gender.Find(id);
            if (master_Gender == null)
            {
                return HttpNotFound();
            }
            return View(master_Gender);
        }

        // POST: Gender/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenderId,Gender")] Master_Gender master_Gender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master_Gender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master_Gender);
        }

        // GET: Gender/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Gender master_Gender = db.Master_Gender.Find(id);
            if (master_Gender == null)
            {
                return HttpNotFound();
            }
            return View(master_Gender);
        }

        // POST: Gender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master_Gender master_Gender = db.Master_Gender.Find(id);
            db.Master_Gender.Remove(master_Gender);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: TestsHosted/List
        public JsonResult List()
        {
            return Json(db.Master_Gender.ToList(), JsonRequestBehavior.AllowGet);
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
