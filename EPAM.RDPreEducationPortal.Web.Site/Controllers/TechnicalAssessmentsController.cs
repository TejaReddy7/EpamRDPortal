using System;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Web.Site.Common;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class TechnicalAssessmentsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: TechnicalAssessments
        public ActionResult Index()
        {
            var accountModel = (AccountModel)Session["UserDetails"];
            return View(db.TechnicalAssessments.Where(x => x.CreatedBy == accountModel.Email).ToList());
        }

        // GET: TechnicalAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalAssessment technicalAssessment = new TechnicalAssessment(); //db.TechnicalAssessments.Find(id);
            technicalAssessment.StudentID = id;
            if (technicalAssessment == null)
            {
                return HttpNotFound();
            }
            //List<TechnicalAssessment> list = new List<TechnicalAssessment>();
            //list = db.TechnicalAssessments.Where(x => x.StudentID == id).ToList();
            return View(technicalAssessment);
        }
        // GET: TechnicalAssessments/Details/5
        public ActionResult DetailsPartialView(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalAssessment technicalAssessment = new TechnicalAssessment(); //db.TechnicalAssessments.Find(id);
            technicalAssessment.StudentID = id;
            if (technicalAssessment == null)
            {
                return HttpNotFound();
            }
            //List<TechnicalAssessment> list = new List<TechnicalAssessment>();
            //list = db.TechnicalAssessments.Where(x => x.StudentID == id).ToList();
            return View("_details", technicalAssessment);
        }

        // GET: TechnicalAssessments/Create
        public ActionResult Create(int candidateId, int testId)
        {
            var model = new TechnicalAssessment { StudentID = candidateId, TestId = testId };
            var ta = db.TechnicalAssessments.FirstOrDefault(x => x.StudentID == candidateId);
            if (ta != null)
            {
                model = ta;
            }
            return View(model);
        }

        // POST: TechnicalAssessments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "TechnicalAssessmentID,TestId,StudentID,Section1Rating,Section1Remarks,Section2Rating,Section2Remarks,Section3Rating,Section3Remarks,Section4Rating,Section4Remarks,Section5Rating,Section5Remarks,EPAMFit,OverallRemarks,OverallRating,AdditionalRemarks,AdditionalRating,HighlyRecommended")] TechnicalAssessment technicalAssessment)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel = (AccountModel)Session["UserDetails"];
                technicalAssessment.CreatedBy = accountModel.Email;
                technicalAssessment.CreatedDate = DateTime.Now;
                technicalAssessment.LastEditedBy = accountModel.FirstName;
                technicalAssessment.LastEditedDate = DateTime.Now;
                technicalAssessment.IsSubmitted = true;
                if (technicalAssessment.TechnicalAssessmentID > 0)
                {
                    db.Entry(technicalAssessment).State = EntityState.Modified;
                }
                else
                {
                    db.TechnicalAssessments.Add(technicalAssessment);
                }
                db.SaveChanges();
                technicalAssessment.SuccessMessage = "Feedback Recorded.";
                //return View(technicalAssessment);
                return RedirectToAction("Index", "CandidateDetails", new { successMessage = "true" });
            }

            return View(technicalAssessment);
        }
        [HttpPost]
        public int Save([Bind(Include = "TechnicalAssessmentID,TestId,StudentID,Section1Rating,Section1Remarks,Section2Rating,Section2Remarks,Section3Rating,Section3Remarks,Section4Rating,Section4Remarks,Section5Rating,Section5Remarks,EPAMFit,OverallRemarks,OverallRating,AdditionalRemarks,AdditionalRating,HighlyRecommended")] TechnicalAssessment technicalAssessment)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel = (AccountModel)Session["UserDetails"];
                technicalAssessment.CreatedBy = accountModel.Email;
                technicalAssessment.CreatedDate = DateTime.Now;
                technicalAssessment.LastEditedBy = accountModel.FirstName;
                technicalAssessment.LastEditedDate = DateTime.Now;
                if (technicalAssessment.TechnicalAssessmentID > 0)
                {
                    db.Entry(technicalAssessment).State = EntityState.Modified;
                }
                else
                {
                    db.TechnicalAssessments.Add(technicalAssessment);
                }
                db.SaveChanges();
            }
            return technicalAssessment.TechnicalAssessmentID;
        }

        // GET: TechnicalAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalAssessment technicalAssessment = db.TechnicalAssessments.Find(id);
            if (technicalAssessment == null)
            {
                return HttpNotFound();
            }
            return View(technicalAssessment);
        }

        // POST: TechnicalAssessments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TechnicalAssessmentID,TestId,StudentID,Section1Rating,Section1Remarks,Section2Rating,Section2Remarks,Section3Rating,Section3Remarks,Section4Rating,Section4Remarks,Section5Rating,Section5Remarks,EPAMFit,OverallRemarks,OverallRating,CreatedBy,LastEditedBy,AdditionalRemarks,AdditionalRating,HighlyRecommended")] TechnicalAssessment technicalAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technicalAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(technicalAssessment);
        }

        // GET: TechnicalAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalAssessment technicalAssessment = db.TechnicalAssessments.Find(id);
            if (technicalAssessment == null)
            {
                return HttpNotFound();
            }
            return View(technicalAssessment);
        }

        // POST: TechnicalAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TechnicalAssessment technicalAssessment = db.TechnicalAssessments.Find(id);
            db.TechnicalAssessments.Remove(technicalAssessment);
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
