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
    public class CandidateMarksController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: CandidateMarks
        public ActionResult Index()
        {
            return View(db.Candidate_Marks.ToList());
        }

        // GET: CandidateMarks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Marks candidate_Marks = db.Candidate_Marks.Find(id);
            if (candidate_Marks == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Marks);
        }

        // GET: CandidateMarks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidateMarks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarksID,CandidateID,TestID,MCQs,CodingChallenge1,CodingChallenge2,CodingChallenge3,IsActive,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate,MarksScored,Result,RoundType")] Candidate_Marks candidate_Marks)
        {
            if (ModelState.IsValid)
            {
                db.Candidate_Marks.Add(candidate_Marks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidate_Marks);
        }

        // GET: CandidateMarks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Marks candidate_Marks = db.Candidate_Marks.Find(id);
            if (candidate_Marks == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Marks);
        }

        // POST: CandidateMarks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarksID,CandidateID,TestID,MCQs,CodingChallenge1,CodingChallenge2,CodingChallenge3,IsActive,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate,MarksScored,Result,RoundType")] Candidate_Marks candidate_Marks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_Marks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate_Marks);
        }

        // GET: CandidateMarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Marks candidate_Marks = db.Candidate_Marks.Find(id);
            if (candidate_Marks == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Marks);
        }

        // POST: CandidateMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate_Marks candidate_Marks = db.Candidate_Marks.Find(id);
            db.Candidate_Marks.Remove(candidate_Marks);
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
