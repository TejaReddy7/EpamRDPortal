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
    public class GroupDiscussionController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: GroupDiscussion
        public ActionResult Index()
        {
            return View(db.Candidate_Details.ToList());
        }

        // GET: GroupDiscussion/Details/5
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

        // GET: GroupDiscussion/Create
        public ActionResult Create(int id)
        {
            var model = new Candidate_Details { CandidateID = id };
            return View(model);
        }

        // POST: GroupDiscussion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateID,IsGDCleared,GDComments,PreferredLocationForSecondRound")] Candidate_Details candidateDetails)
        {
            if (ModelState.IsValid)
            {
                var result = db.Candidate_Details.SingleOrDefault(b => b.CandidateID == candidateDetails.CandidateID);
                //db.Candidate_Details.Add(candidate_Details);
                result.GDComments = candidateDetails.GDComments;
                result.IsGDCleared = candidateDetails.IsGDCleared;
                //db.Entry(candidate_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CandidateDetails", new { successMessage = "true" });
                //return RedirectToAction("Index");
            }

            return View(candidateDetails);
        }
        //[HttpPost]
        //public ActionResult List(List<String> accepts, List<String> rejects)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (accepts != null)
        //            foreach (var item in accepts)
        //            {
        //                var id = Convert.ToInt32(item);
        //                var result = db.Candidate_Details.SingleOrDefault(b => b.CandidateID == id);
        //                if (result != null)
        //                {
        //                    result.GDComments = "Cleared";
        //                    result.IsGDCleared = true;
        //                    db.SaveChanges();
        //                }
        //            }

        //        if (rejects != null)
        //            foreach (var item in rejects)
        //            {
        //                var id = Convert.ToInt32(item);
        //                var result = db.Candidate_Details.SingleOrDefault(b => b.CandidateID == id);
        //                if (result != null)
        //                {
        //                    result.GDComments = "Rejected";
        //                    result.IsGDCleared = false;
        //                    db.SaveChanges();
        //                }
        //            }
        //        return RedirectToAction("Index", "CandidateDetails", new { successMessage = "true" });
        //    }
        //    return View();
        //} 
        public bool Update(int id, string status)
        {
            var result = db.Candidate_Details.SingleOrDefault(b => b.CandidateID == id);
            if (result != null)
            {
                if (status == "Accepted")
                {
                    result.GDComments = "Accepted";
                    result.IsGDCleared = true;
                }
                else
                {
                    result.GDComments = "Rejected";
                    result.IsGDCleared = false;
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        // GET: GroupDiscussion/Edit/5
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

        // POST: GroupDiscussion/Edit/5
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

        // GET: GroupDiscussion/Delete/5
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

        // POST: GroupDiscussion/Delete/5
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
