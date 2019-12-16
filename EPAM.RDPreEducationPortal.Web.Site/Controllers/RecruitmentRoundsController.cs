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
    public class RecruitmentRoundsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: RecruitmentRounds
        public ActionResult Index()
        {
            return View(db.Recruitment_Rounds.ToList());
        }

        // GET: RecruitmentRounds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment_Rounds recruitment_Rounds = db.Recruitment_Rounds.Find(id);
            if (recruitment_Rounds == null)
            {
                return HttpNotFound();
            }
            return View(recruitment_Rounds);
        }

        // GET: RecruitmentRounds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecruitmentRounds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoundID,RoundName,RoundKey,IsActive")] Recruitment_Rounds recruitment_Rounds)
        {
            if (ModelState.IsValid)
            {
                db.Recruitment_Rounds.Add(recruitment_Rounds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recruitment_Rounds);
        }

        // GET: RecruitmentRounds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment_Rounds recruitment_Rounds = db.Recruitment_Rounds.Find(id);
            if (recruitment_Rounds == null)
            {
                return HttpNotFound();
            }
            return View(recruitment_Rounds);
        }

        // POST: RecruitmentRounds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoundID,RoundName,RoundKey,IsActive")] Recruitment_Rounds recruitment_Rounds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruitment_Rounds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruitment_Rounds);
        }

        // GET: RecruitmentRounds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment_Rounds recruitment_Rounds = db.Recruitment_Rounds.Find(id);
            if (recruitment_Rounds == null)
            {
                return HttpNotFound();
            }
            return View(recruitment_Rounds);
        }

        // POST: RecruitmentRounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recruitment_Rounds recruitment_Rounds = db.Recruitment_Rounds.Find(id);
            db.Recruitment_Rounds.Remove(recruitment_Rounds);
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
