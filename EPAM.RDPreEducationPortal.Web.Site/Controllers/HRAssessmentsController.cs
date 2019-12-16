using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Web.Site.Common;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class HRAssessmentsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: HRAssessments
        public ActionResult Index()
        {
            var accountModel = (AccountModel)Session["UserDetails"];
            return View(db.HRAssessments.Where(x => x.CreatedBy == accountModel.Email).ToList());
        }

        // GET: HRAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRAssessment hRAssessment = db.HRAssessments.Find(id);
            if (hRAssessment == null)
            {
                return HttpNotFound();
            }
            return View(hRAssessment);
        }

        // GET: HRAssessments/Create
        public ActionResult Create(int candidateId, int testId)
        {
            var model = new HRAssessment { StudentID = candidateId, TestId = testId };
            var hra = db.HRAssessments.FirstOrDefault(x => x.StudentID == candidateId);
            if (hra != null)
            {
                model = hra;
            }
            return View(model);
        }

        // POST: HRAssessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "HRAssessmentID,TestId,StudentID,PresentationSkills,ProfessionalImpression,SelfManagement,CommunicationInterpersonalSkills,StressTolerance,DecisionMaking,Learnability,OrganizationalFit,Creative,Expert,Original,Candid,Intelligent,Driven,ValueTheIndividual,FocusOnCustomer,ActAsATeam,StriveForExcellence,ActWithIntegrity,Humble,Hungry,Smart,SignificantAchievements,PlansForFurtherStudies,CareerPlans,OverallRating,OverallComments,Recommendation,HighlyRecommended")] HRAssessment hRAssessment)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel = (AccountModel)Session["UserDetails"];
                hRAssessment.CreatedBy = accountModel.Email;
                hRAssessment.CreatedDate = DateTime.Now;
                hRAssessment.LastEditedBy = accountModel.Email;
                hRAssessment.LastEditedDate = DateTime.Now;
                hRAssessment.IsSubmitted = true;
                if (hRAssessment.HRAssessmentID > 0)
                {
                    db.Entry(hRAssessment).State = EntityState.Modified;
                }
                else
                {
                    db.HRAssessments.Add(hRAssessment);
                }
                db.SaveChanges();
                hRAssessment.SuccessMessage = "Feedback Recorded.";
                //return View(hRAssessment);
                return RedirectToAction("Index", "CandidateDetails", new { successMessage = "true" });
            }

            return View(hRAssessment);
        }

        [HttpPost]
        public int Save(HRAssessment hRAssessment)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel = (AccountModel)Session["UserDetails"];
                hRAssessment.CreatedBy = accountModel.Email;
                hRAssessment.CreatedDate = DateTime.Now;
                hRAssessment.LastEditedBy = accountModel.Email;
                hRAssessment.LastEditedDate = DateTime.Now;
                if (hRAssessment.HRAssessmentID > 0)
                {
                    db.Entry(hRAssessment).State = EntityState.Modified;
                }
                else
                {
                    db.HRAssessments.Add(hRAssessment);
                }
                db.SaveChanges();
                hRAssessment.SuccessMessage = "Feedback Saved successfully as draft.";
            }

            return hRAssessment.HRAssessmentID;
        }
        // GET: HRAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRAssessment hRAssessment = db.HRAssessments.Find(id);
            if (hRAssessment == null)
            {
                return HttpNotFound();
            }
            return View(hRAssessment);
        }

        // POST: HRAssessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HRAssessmentID,TestId,StudentID,PresentationSkills,ProfessionalImpression,SelfManagement,CommunicationInterpersonalSkills,StressTolerance,DecisionMaking,Learnability,OrganizationalFit,Creative,Expert,Original,Candid,Intelligent,Driven,ValueTheIndividual,FocusOnCustomer,ActAsATeam,StriveForExcellence,ActWithIntegrity,Humble,Hungry,Smart,SignificantAchievements,PlansForFurtherStudies,CareerPlans,OverallRating,OverallComments,Recommendation,CreatedBy,LastEditedBy,HighlyRecommended")] HRAssessment hRAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hRAssessment);
        }

        // GET: HRAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRAssessment hRAssessment = db.HRAssessments.Find(id);
            if (hRAssessment == null)
            {
                return HttpNotFound();
            }
            return View(hRAssessment);
        }

        // POST: HRAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HRAssessment hRAssessment = db.HRAssessments.Find(id);
            db.HRAssessments.Remove(hRAssessment);
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
