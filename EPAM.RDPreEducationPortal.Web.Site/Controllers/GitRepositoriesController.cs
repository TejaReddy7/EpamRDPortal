using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.Common;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class GitRepositoriesController : Controller
    {
        private GitRepositoriesModel _model = new GitRepositoriesModel();
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: GitRepositories
        public ActionResult Index()
        {
            var model = new Student_GitRepositories();
            return View(model);
        }

        // GET: GitRepositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_GitRepositories student_GitRepositories = db.Student_GitRepositories.Find(id);
            if (student_GitRepositories == null)
            {
                return HttpNotFound();
            }
            return View(student_GitRepositories);
        }

        // GET: GitRepositories/Create
        public ActionResult Create()
        {
            Student_GitRepositories model = new Student_GitRepositories();
            return View(model);
        }

        // POST: GitRepositories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GitRepositoryID,StudentID,RepositoryUrl,Status,ProjectName,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate,TaskId,Comments")] Student_GitRepositories studentGitRepositories)
        {
            if (ModelState.IsValid)
            {
                studentGitRepositories.ProjectName = studentGitRepositories.RepositoryUrl.Split('/')[studentGitRepositories.RepositoryUrl.Split('/').Length - 1].Split('.').First();
                studentGitRepositories.StudentID = UserDetailsSingleton.Instance.UserId;
                studentGitRepositories.Status = true;
                studentGitRepositories.CreatedBy = Session["Name"].ToString();
                studentGitRepositories.CreatedDate = DateTime.Now;
                studentGitRepositories.LastEditedBy = Session["Name"].ToString();
                studentGitRepositories.LastEditedDate = DateTime.Now;
                db.Student_GitRepositories.Add(studentGitRepositories);
                db.SaveChanges();
                var model = new GitRepositoriesModel();
                model.CloneRepository(studentGitRepositories.RepositoryUrl);
                model.RunJenkin(studentGitRepositories.RepositoryUrl);

                string taskSubmissionEmailTemplate = Server.MapPath("~/EmailTemplates/TaskSubmissionTemplate.html");
                string logoPath = Server.MapPath("~/Content/Images/logo.png");
                string epamTrainingCenterlogoPath = Server.MapPath("~/Content/Images/EpamTrainingCenterLogo.png");
                string taskName = "Collections";//studentGitRepositories.TaskName;
                model.SendTaskSubmissionEmail(taskName, taskSubmissionEmailTemplate, logoPath, epamTrainingCenterlogoPath);
                Student_GitRepositories repoModel =
                    new Student_GitRepositories {};
                return View("Index", repoModel);
            }

            return View(studentGitRepositories);
        }

        // GET: GitRepositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_GitRepositories student_GitRepositories = db.Student_GitRepositories.Find(id);
            if (student_GitRepositories == null)
            {
                return HttpNotFound();
            }
            return View(student_GitRepositories);
        }

        // POST: GitRepositories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GitRepositoryID,StudentID,RepositoryUrl,Status,ProjectName,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate,TaskId,Comments")] Student_GitRepositories student_GitRepositories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_GitRepositories).State = EntityState.Modified;
                db.SaveChanges();
                GitRepositoriesModel model = new GitRepositoriesModel();
                //model.GenerateJenkinsfile(student_GitRepositories.RepositoryUrl);
                return RedirectToAction("Index");
            }
            return View(student_GitRepositories);
        }

        // GET: GitRepositories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_GitRepositories student_GitRepositories = db.Student_GitRepositories.Find(id);
            if (student_GitRepositories == null)
            {
                return HttpNotFound();
            }
            return View(student_GitRepositories);
        }

        // POST: GitRepositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_GitRepositories student_GitRepositories = db.Student_GitRepositories.Find(id);
            db.Student_GitRepositories.Remove(student_GitRepositories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BulkUpload()
        {
            return View(_model);
        }
        [HttpPost]
        public ActionResult BulkUpload(HttpPostedFileBase upload, FormCollection collection)
        {
            try
            {
                DataSet result = new DataSet();

                if (Request.Files[0].ContentLength > 0)
                {
                    string fileExtension =
                                         System.IO.Path.GetExtension(Request.Files[0].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
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
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
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
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();
                        result.ReadXml(xmlreader);
                        xmlreader.Close();
                    }
                    if (result.Tables.Count > 0)
                    {
                        foreach (DataRow dr in result.Tables[0].Rows)
                        {
                            if (!db.Student_GitRepositories.ToList()
                                .Exists(x => x.RepositoryUrl.Contains(dr["GIT URL"].ToString())))
                            {
                                var user = db.Users.ToList().Find(x => x.Email.Contains(dr["Email2"].ToString()));
                                if (user != null)
                                {
                                    int studentId = user.UserId;
                                    var find = db.Tasks.ToList().Find(x => x.TaskName.Contains(dr["Task Name"].ToString()));
                                    if (find != null)
                                    {
                                        int taskId = find.TaskId;

                                        Student_GitRepositories studentGitRepositories = new Student_GitRepositories
                                        {
                                            ProjectName =
                                                dr["GIT URL"].ToString().Split('/')[dr["GIT URL"].ToString().Split('/').Length - 1]
                                                    .Split('.').First(),
                                            RepositoryUrl = dr["GIT URL"].ToString(),
                                            StudentID = studentId,
                                            Status = true,
                                            TaskId = taskId,
                                            CreatedBy = UserDetailsSingleton.Instance.Name,
                                            CreatedDate = DateTime.Now,
                                            LastEditedBy = UserDetailsSingleton.Instance.Name,
                                            LastEditedDate = DateTime.Now
                                        };
                                        db.Student_GitRepositories.Add(studentGitRepositories);

                                        db.SaveChanges();
                                        var model = new GitRepositoriesModel();
                                        model.CloneRepository(dr["GIT URL"].ToString());
                                        model.RunJenkin(dr["GIT URL"].ToString());

                                        string taskSubmissionEmailTemplate = Server.MapPath("~/EmailTemplates/TaskSubmissionTemplate.html");
                                        string logoPath = Server.MapPath("~/Content/Images/logo.png");
                                        string epamTrainingCenterlogoPath = Server.MapPath("~/Content/Images/EpamTrainingCenterLogo.png");
                                        //studentGitRepositories.TaskName;
                                        model.SendTaskSubmissionEmail(dr["Task Name"].ToString(), taskSubmissionEmailTemplate, logoPath, epamTrainingCenterlogoPath);
                                    }
                                }
                            }
                        }
                    }
                }
                _model.SuccessMessage = "Git repositories are uploaded successfully.";
            }
            catch (Exception ex)
            {
                _model.ErrorMessage = ex.Message;
            }
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
