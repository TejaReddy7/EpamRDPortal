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
    public class RecruitmentsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Recruitments
        public ActionResult Index()
        {
            return View(db.Recruitments.ToList());
        }

        // GET: Recruitments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment recruitment = db.Recruitments.Find(id);
            if (recruitment == null)
            {
                return HttpNotFound();
            }
            return View(recruitment);
        }

        // GET: Recruitments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recruitments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecruitmentID,Name,Comments")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                db.Recruitments.Add(recruitment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recruitment);
        }

        // GET: Recruitments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment recruitment = db.Recruitments.Find(id);
            if (recruitment == null)
            {
                return HttpNotFound();
            }
            return View(recruitment);
        }

        // POST: Recruitments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecruitmentID,Name,Comments")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruitment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruitment);
        }

        // GET: Recruitments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment recruitment = db.Recruitments.Find(id);
            if (recruitment == null)
            {
                return HttpNotFound();
            }
            return View(recruitment);
        }

        // POST: Recruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recruitment recruitment = db.Recruitments.Find(id);
            db.Recruitments.Remove(recruitment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Recruitments/List
        public JsonResult List()
        { 
            return Json(db.Recruitments.ToList(), JsonRequestBehavior.AllowGet);
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
