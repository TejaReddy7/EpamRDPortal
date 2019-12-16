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
    public class TestsHostedController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: TestsHosted
        public ActionResult Index()
        {
            return View(db.TestsHosteds.ToList());
        }

        // GET: TestsHosted/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsHosted testsHosted = db.TestsHosteds.Find(id);
            if (testsHosted == null)
            {
                return HttpNotFound();
            }
            return View(testsHosted);
        }

        // GET: TestsHosted/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestsHosted/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecruitmentId,TestName,LocationId,TestKey,CutoffPercentage,CodingWeightageOnSelection1,TechnicalWeightageOnSelection,HRWeightageOnSelection")] TestsHosted testsHosted)
        {
            if (ModelState.IsValid)
            {
                db.TestsHosteds.Add(testsHosted);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testsHosted);
        }

        // GET: TestsHosted/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsHosted testsHosted = db.TestsHosteds.Find(id);
            if (testsHosted == null)
            {
                return HttpNotFound();
            }
            return View(testsHosted);
        }

        // POST: TestsHosted/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestId,RecruitmentId,TestName,StartDate,EndDate,TotalScore,Duration,LocationId,TestKey,CutoffPercentage,WeightageOnSelection,CodingWeightageOnSelection1,TechnicalWeightageOnSelection,HRWeightageOnSelection")] TestsHosted testsHosted)
        {
            if (testsHosted.LocationId == 0)
            {
                ModelState.AddModelError("LocationId", "Please choose location");
            }
            if (testsHosted.RecruitmentId == 0)
            {
                ModelState.AddModelError("RecruitmentId", "Please choose Recruitment");
            }
            if (ModelState.IsValid)
            {
                db.Entry(testsHosted).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testsHosted);
        }

        // GET: TestsHosted/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestsHosted testsHosted = db.TestsHosteds.Find(id);
            if (testsHosted == null)
            {
                return HttpNotFound();
            }
            return View(testsHosted);
        }

        // POST: TestsHosted/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestsHosted testsHosted = db.TestsHosteds.Find(id);
            db.TestsHosteds.Remove(testsHosted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TestsHosted/List
        public JsonResult List(int? recruitmentId)
        {
            if (recruitmentId != null)
                return Json(db.TestsHosteds.Where(x => x.RecruitmentId == (int)recruitmentId).ToList(), JsonRequestBehavior.AllowGet);
            else
                return Json(db.TestsHosteds.ToList(), JsonRequestBehavior.AllowGet);
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
