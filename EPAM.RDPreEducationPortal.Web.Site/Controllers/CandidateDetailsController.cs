using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class CandidateDetailsController : Controller
    {
        private readonly Candidate_Details _model = new Candidate_Details();
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: CandidateDetails
        public ActionResult Index(bool? successMessage)
        {
            if (successMessage != null && successMessage == true)
            {
                _model.SuccessMessage = "Feedback Recorded successfully.";
            }
            //db.Candidate_Details.ToList()
            return View(_model);
        }
        // GET: CandidateDetails/HREligible
        public ActionResult HREligible()
        {
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Candidate_Details_HREligible");
            var myData = ds.Tables[0].AsEnumerable().Select(r => new Candidate_Details
            {
                CandidateID = r.Field<int>("CandidateID"),
                CandidateName = r.Field<string>("CandidateName"),
                EmailAddress = r.Field<string>("CandidateEmail"),
                Gender = r.Field<string>("Gender"),
                //MarksScored = r.Field<double>("MarksScored"),
                FirstRoundCodingMarks = r.Field<string>("FirstRoundMarks"),
                MobileNumber = r.Field<string>("MobileNumber"),
                Status = r.Field<string>("Status"),
                SecondRoundMarks = r.Field<string>("SecondRoundMarks"),
                TestId = r.Field<int>("TestId"),
                TRInterviewer = r.Field<string>("TRInterviewer"),
                TRScore = r.Field<int>("TRScore"),
                PermanentStateOfResidence = r.Field<string>("PermanentStateOfResidence")
            });
            var s = myData.ToList();
            return View(s);
        }
        // GET: CandidateDetails/Details/5
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

        // GET: CandidateDetails
        public ActionResult List(int? testId)
        {
            var role = "";
            if (Session["Role"] != null)
            {
                role = Session["Role"].ToString();
            }
            AccountModel accountModel = new AccountModel();
            if (Session["UserDetails"] != null)
            {
                accountModel = (AccountModel)Session["UserDetails"];
            }

            if (accountModel.Role == "TechnicalInterviewer" || accountModel.Role == "GDManager")
            {
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@TestId", testId);
                parameters[1] = new SqlParameter("@Role", role);
                DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Candidate_Details", parameters);
                var myData = ds.Tables[0].AsEnumerable().Select(r => new Candidate_Details
                {
                    CandidateID = r.Field<int>("CandidateID"),
                    CandidateName = r.Field<string>("CandidateName"),
                    EmailAddress = r.Field<string>("CandidateEmail"),
                    Gender = r.Field<string>("Gender"),
                    //MarksScored = r.Field<double>("MarksScored"),
                    FirstRoundCodingMarks = r.Field<string>("FirstRoundMarks"),
                    MobileNumber = r.Field<string>("MobileNumber"),
                    Status = r.Field<string>("Status"),
                    SecondRoundMarks = r.Field<string>("SecondRoundMarks"),
                    TestId = r.Field<int>("TestId"),
                    TRInterviewer = r.Field<string>("TRInterviewer"),
                    //TRScore = r.Field<int>("TRScore"),
                    PermanentStateOfResidence = r.Field<string>("PermanentStateOfResidence"),
                    GDStatus = r.Field<int>("GDStatus")
                });
                var s = myData.ToList();
                return View(s);
            }
            else if (accountModel.Role == "HR")
            {
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@TestId", testId);
                DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Candidate_Details_HREligible", parameters);
                var myData = ds.Tables[0].AsEnumerable().Select(r => new Candidate_Details
                {
                    CandidateID = r.Field<int>("CandidateID"),
                    CandidateName = r.Field<string>("CandidateName"),
                    EmailAddress = r.Field<string>("CandidateEmail"),
                    Gender = r.Field<string>("Gender"),
                    //MarksScored = r.Field<double>("MarksScored"),
                    FirstRoundCodingMarks = r.Field<string>("FirstRoundMarks"),
                    MobileNumber = r.Field<string>("MobileNumber"),
                    Status = r.Field<string>("Status"),
                    SecondRoundMarks = r.Field<string>("SecondRoundMarks"),
                    TestId = r.Field<int>("TestId"),
                    TRInterviewer = r.Field<string>("TRInterviewer"),
                    TRScore = r.Field<int>("TRScore"),
                    PermanentStateOfResidence = r.Field<string>("PermanentStateOfResidence")
                });
                var s = myData.ToList();
                return View("HREligible", s);
            }
            return null;
        }

        // GET: CandidateDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CandidateDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateID,CandidateName,CandidateLocationID,CandidateEmail,ContactNumber,CandidateResume,StartedOn,StartingTime,PreviousAssessments,Duration,MarksScored,Result,Report,PublicReport,FirstName,LastName,EmailAddress,MobileNumber,GraduationCollegeName,Graduation,GraduationSpecialty,YearOfPassedOut,GraduationMarksCGPA,C12MarksCGPA,C10MarksCGPA,Gender,PermanentAddress,PermanentStateOfResidence,CurrentAddress,GovernmentIssuedIDProof,PreferredJobLocation")] Candidate_Details candidate_Details)
        {
            if (ModelState.IsValid)
            {
                db.Candidate_Details.Add(candidate_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(candidate_Details);
        }

        // GET: CandidateDetails/Edit/5
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

        // POST: CandidateDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateID,CandidateName,CandidateLocationID,CandidateEmail,ContactNumber,CandidateResume,StartedOn,StartingTime,PreviousAssessments,Duration,MarksScored,Result,Report,PublicReport,FirstName,LastName,EmailAddress,MobileNumber,GraduationCollegeName,Graduation,GraduationSpecialty,YearOfPassedOut,GraduationMarksCGPA,C12MarksCGPA,C10MarksCGPA,Gender,PermanentAddress,PermanentStateOfResidence,CurrentAddress,GovernmentIssuedIDProof,PreferredJobLocation,CreatedBy,LastEditedBy")] Candidate_Details candidate_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate_Details);
        }

        // GET: CandidateDetails/Delete/5
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

        // POST: CandidateDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate_Details candidate_Details = db.Candidate_Details.Find(id);
            db.Candidate_Details.Remove(candidate_Details);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadCandidates()
        {
            Candidate_Details model = new Candidate_Details();
            return View(model);
        }
        [HttpPost]
        public ActionResult UploadCandidates(HttpPostedFileBase upload, FormCollection collection, bool isFirstRound)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                string fileExtension = Path.GetExtension(Request.Files[0].FileName);

                DataSet result = new DataSet();
                if (fileExtension == ".csv" || fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files[0].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files[0].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx" || fileExtension == ".csv")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();

                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    excelConnection.Close();
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    //excel data saves in temp file here.
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(result);
                    }
                }
                if (result.Tables.Count > 0)
                {
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        //try
                        //{
                        var candidateID = 0;
                        var candidateMarks = new Candidate_Marks();
                        string email = dr["Candidate Email"].ToString();
                        var candidate = db.Candidate_Details.FirstOrDefault(x => x.CandidateEmail == email);
                        if (candidate == null)//Convert.ToBoolean(isFirstRound) == true)
                        {
                            var resume = "";
                            var report = "";
                            if (dr.Table.Columns.Contains("Candidate Resume") && !string.IsNullOrEmpty(dr["Candidate Resume"].ToString()))
                            {
                                resume = dr["Candidate Resume"].ToString();
                            }
                            if (dr.Table.Columns.Contains("Public Report") && !string.IsNullOrEmpty(dr["Public Report"].ToString()))
                            {
                                report = dr["Public Report"].ToString();
                            }
                            bool isPEPStudnet = false;
                            if (Convert.ToInt32(collection["TestId"]) == 0)
                                isPEPStudnet = true;

                            //double v = 0;
                            //Double.TryParse(dr["Marks Scored"].ToString(), out v);
                            //var _result = false;
                            //if (dr["Result"].ToString() == "Passed")
                            //    _result = true;
                            Candidate_Details candidateDetails = new Candidate_Details
                            {
                                CandidateName = dr["Candidate Name"].ToString(),
                                CandidateResume = resume,
                                //PublicReport = report,
                                CandidateEmail = dr["Email Address"].ToString(),
                                EmailAddress = dr["Email Address"].ToString(),
                                MobileNumber = dr["Mobile Number"].ToString(),
                                ContactNumber = dr["Mobile Number"].ToString(),
                                Duration = dr["Duration"].ToString(),
                                GraduationCollegeName = dr["Graduation College Name"].ToString().Length <= 99 ? dr["Graduation College Name"].ToString() : dr["Graduation College Name"].ToString().Substring(0, 99),
                                Graduation = dr["Graduation"].ToString(),
                                GraduationSpecialty = dr["Graduation Speciality"].ToString(),
                                TestId = Convert.ToInt32(collection["TestId"]),
                                RecruitmentId = Convert.ToInt32(collection["RecruitmentId"]),
                                Gender = dr["Gender"].ToString(),
                                IsPEPStudent = isPEPStudnet,
                                //PreviousAssessment = dr["Previous Assessments"].ToString(),
                                PreviousAssessment = (dr.Table.Columns.Contains("Previous Assessments") && !string.IsNullOrEmpty(dr["Previous Assessments"].ToString())) ? dr["Previous Assessments"].ToString() : "",
                                Report = dr["Report"].ToString(),
                                GraduationMarksCGPA = dr.Table.Columns.Contains("Graduation Marks (C G P A)") ? dr["Graduation Marks (C G P A)"].ToString() : "",
                                C12MarksCGPA = dr.Table.Columns.Contains("10+2 Marks (C G P A)") ? dr["10+2 Marks (C G P A)"].ToString() : "",
                                C10MarksCGPA = dr.Table.Columns.Contains("10th Marks (C G P A)") ? dr["10th Marks (C G P A)"].ToString() : "",
                                PermanentAddress = dr.Table.Columns.Contains("Permanent Address") ? dr["Permanent Address"].ToString() : "",
                                PermanentStateOfResidence = dr.Table.Columns.Contains("Permanent State Of Residence") ? dr["Permanent State Of Residence"].ToString() : "",
                                CurrentAddress = dr.Table.Columns.Contains("Current Address") ? dr["Current Address"].ToString() : "",
                                GovernmentIssuedIDProof = dr.Table.Columns.Contains("Government Issued I D Proof") ? dr["Government Issued I D Proof"].ToString() : "",
                                PreferredJobLocation = dr.Table.Columns.Contains("Preferred Location For Second ###") ? dr["Preferred Location For Second ###"].ToString() : "",
                                PreferredLocationForSecondRound = dr.Table.Columns.Contains("Preferred Location For Second ###") ? dr["Preferred Location For Second ###"].ToString() : "",
                                IsGDCleared = true,
                                GDComments = "Cleared"
                            };
                            if (Convert.ToBoolean(isFirstRound) == true)
                            {
                                candidateDetails.PublicReport = report;
                            }
                            else
                            {
                                candidateDetails.SecondRoundPublicReport = report;
                            }
                            db.Candidate_Details.Add(candidateDetails);
                            db.SaveChanges();
                            candidateID = candidateDetails.CandidateID;
                            candidateMarks.CandidateID = candidateDetails.CandidateID;
                            candidateMarks.TestID = Convert.ToInt32(collection["TestId"]);
                            if (dr.Table.Columns.Contains("M C Qs") && !string.IsNullOrEmpty(dr["M C Qs"].ToString()))
                                candidateMarks.MCQs = Convert.ToDouble(dr["M C Qs"].ToString());
                            if (dr.Table.Columns.Contains("Coding Challenge - 1") && !string.IsNullOrEmpty(dr["Coding Challenge - 1"].ToString()))
                                candidateMarks.CodingChallenge1 = Convert.ToDouble(dr["Coding Challenge - 1"]);
                            if (dr.Table.Columns.Contains("Coding Challenge - 2") && !string.IsNullOrEmpty(dr["Coding Challenge - 2"].ToString()))
                                candidateMarks.CodingChallenge2 = Convert.ToDouble(dr["Coding Challenge - 2"]);
                            if (dr.Table.Columns.Contains("Coding Challenge - 3") && !string.IsNullOrEmpty(dr["Coding Challenge - 3"].ToString()))
                                candidateMarks.CodingChallenge3 = Convert.ToDouble(dr["Coding Challenge - 3"]);
                            candidateMarks.MarksScored = dr["Marks Scored"].ToString();
                            candidateMarks.Result = dr["Result"].ToString();
                            if (Convert.ToBoolean(isFirstRound) == true)
                            {
                                candidateMarks.RoundType = "FirstRound";
                            }
                            else
                            {
                                candidateMarks.RoundType = "Direct";
                            }
                            db.Candidate_Marks.Add(candidateMarks);
                            db.SaveChanges();
                        }
                        else
                        {
                            var resume = "";
                            if (dr.Table.Columns.Contains("Candidate Resume") && !string.IsNullOrEmpty(dr["Candidate Resume"].ToString()))
                            {
                                resume = dr["Candidate Resume"].ToString();
                            }
                            string candidateEmail = dr["Candidate Email"].ToString();
                            var candidate_Details = db.Candidate_Details.FirstOrDefault(x => x.CandidateEmail == candidateEmail);
                            if (candidate_Details != null)
                            {
                                candidate_Details.SecondRoundPublicReport = dr["Public Report"].ToString();
                                candidate_Details.CandidateResume = resume;
                                candidate_Details.IsGDCleared = true;
                                candidate_Details.GDComments = "Cleared";
                                if (candidateID == 0)
                                {
                                    var id = (candidate_Details != null) ? candidate_Details.CandidateID : 0;
                                    candidateMarks.CandidateID = Convert.ToInt32(id);
                                }
                                else
                                {
                                    candidateMarks.CandidateID = candidateID;
                                }
                                db.Entry(candidate_Details).State = EntityState.Modified;
                                db.SaveChanges();

                                candidateMarks.TestID = Convert.ToInt32(collection["TestId"]);
                                if (dr.Table.Columns.Contains("M C Qs") && !string.IsNullOrEmpty(dr["M C Qs"].ToString()))
                                    candidateMarks.MCQs = Convert.ToDouble(dr["M C Qs"].ToString());
                                if (dr.Table.Columns.Contains("Coding Challenge - 1") && !string.IsNullOrEmpty(dr["Coding Challenge - 1"].ToString()))
                                    candidateMarks.CodingChallenge1 = Convert.ToDouble(dr["Coding Challenge - 1"]);
                                if (dr.Table.Columns.Contains("Coding Challenge - 2") && !string.IsNullOrEmpty(dr["Coding Challenge - 2"].ToString()))
                                    candidateMarks.CodingChallenge2 = Convert.ToDouble(dr["Coding Challenge - 2"]);
                                if (dr.Table.Columns.Contains("Coding Challenge - 3") && !string.IsNullOrEmpty(dr["Coding Challenge - 3"].ToString()))
                                    candidateMarks.CodingChallenge3 = Convert.ToDouble(dr["Coding Challenge - 3"]);
                                candidateMarks.MarksScored = dr["Marks Scored"].ToString();
                                candidateMarks.Result = dr["Result"].ToString();
                                candidateMarks.RoundType = "SecondRound";
                                db.Candidate_Marks.Add(candidateMarks);
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            _model.SuccessMessage = "Coding Challenge result uploaded successfully.";
            return View(_model);
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
