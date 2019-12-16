using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using EPAM.RDPreEducationPortal.Web.Site.Handlers;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{

    public class CollegesController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Colleges
        public ActionResult Index()
        {
            return View(db.Colleges.ToList());
        }

        // GET: Colleges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = db.Colleges.Find(id);
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // GET: Colleges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colleges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollegeID,CollegeName,IsActive")] College college)
        {
            if (ModelState.IsValid)
            {
                db.Colleges.Add(college);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(college);
        }

        // GET: Colleges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = db.Colleges.Find(id);
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // POST: Colleges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollegeID,CollegeName,IsActive")] College college)
        {
            if (ModelState.IsValid)
            {
                db.Entry(college).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(college);
        }

        // GET: Colleges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            College college = db.Colleges.Find(id);
            if (college == null)
            {
                return HttpNotFound();
            }
            return View(college);
        }

        // POST: Colleges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            College college = db.Colleges.Find(id);
            db.Colleges.Remove(college);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdateCandidateColleges()
        {
            //var parameters = new SqlParameter[1];
            //parameters[0] = new SqlParameter("@RecruitmentId", recruitmentId);
            CollegeModel model = new CollegeModel();
            return View(model);
        }

        public int UpdateCollege(string oldCollegeName, string newCollegeName)
        {
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@oldCollegeName", oldCollegeName.Trim());
            parameters[1] = new SqlParameter("@newCollegeName", newCollegeName);
            //oldCollegeName.Trim()
            var id = SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Update_CandidateGraduationCollegeName", parameters);
            return id;
        }
        public JsonResult GetColleges(string value)
        {
            List<CollegeModel> list = new List<CollegeModel>();
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@key", value);
            //oldCollegeName.Trim()
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_CollegesByKey", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CollegeModel cmodel = new CollegeModel();
                cmodel.CollegeName = dr["CollegeName"].ToString();
                list.Add(cmodel);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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
