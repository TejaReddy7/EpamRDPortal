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
    public class Candidate_DetailsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Candidate_Details
        public ActionResult Index()
        {
            return View(db.Candidate_Details.ToList());
        }

        // GET: Candidate_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Details candidate_Details = db.Candidate_Details.Find(id);
            if (candidate_Details == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Details);
        }

        // GET: Candidate_Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate_Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateID,CandidateName,CandidateLocationID,CandidateEmail,ContactNumber,CandidateResume,StartedOn,StartingTime,PreviousAssessments,Duration,MarksScored,Result,Report,PublicReport,FirstName,LastName,EmailAddress,MobileNumber,GraduationCollegeName,Graduation,GraduationSpecialty,YearOfPassedOut,GraduationMarksCGPA,C12MarksCGPA,C10MarksCGPA,Gender,PermanentAddress,PermanentStateOfResidence,CurrentAddress,GovernmentIssuedIDProof,PreferredJobLocation,RecruitmentId,TestId,SecondRoundPublicReport,IsPEPStudent,IsCodathonWinner,IsGDCleared,GDComments,PreferredLocationForSecondRound")] Candidate_Details candidate_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidate_Details);
        }

        // GET: Candidate_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Details candidate_Details = db.Candidate_Details.Find(id);
            if (candidate_Details == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Details);
        }

        // POST: Candidate_Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateID,CandidateName,CandidateLocationID,CandidateEmail,ContactNumber,CandidateResume,StartedOn,StartingTime,PreviousAssessments,Duration,MarksScored,Result,Report,PublicReport,FirstName,LastName,EmailAddress,MobileNumber,GraduationCollegeName,Graduation,GraduationSpecialty,YearOfPassedOut,GraduationMarksCGPA,C12MarksCGPA,C10MarksCGPA,Gender,PermanentAddress,PermanentStateOfResidence,CurrentAddress,GovernmentIssuedIDProof,PreferredJobLocation,RecruitmentId,TestId,SecondRoundPublicReport,IsPEPStudent,IsCodathonWinner,IsGDCleared,GDComments,PreferredLocationForSecondRound")] Candidate_Details candidate_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate_Details);
        }

        // GET: Candidate_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate_Details candidate_Details = db.Candidate_Details.Find(id);
            if (candidate_Details == null)
            {
                return HttpNotFound();
            }
            return View(candidate_Details);
        }

        // POST: Candidate_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate_Details candidate_Details = db.Candidate_Details.Find(id);
            db.Candidate_Details.Remove(candidate_Details);
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
