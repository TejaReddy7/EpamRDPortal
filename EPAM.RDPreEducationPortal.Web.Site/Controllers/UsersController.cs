using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class UsersController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();
        readonly User _model = new User();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.OrderByDescending(x => x.UserId).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View(_model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,RoleId,FirstName,MiddleName,LastName,GenderId,Email,ContactNumber,Address,CollegeId,MarksScored,GraduationSpecialty,EmailAddressToSendNotifications,Active,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate")] User user)
        {
            user.UserName = user.Email;
            if (ModelState.IsValid)
            {
                user.Password = Guid.NewGuid().ToString("n").Substring(0, 8);
                db.Users.Add(user);
                db.SaveChanges();

                var htmlTemplateUrl = Server.MapPath("~/EmailTemplates/NewUser.html");
                var logoPath = Server.MapPath("~/Content/Images/logo.png");
                var epamTrainingCenterlogoPath = Server.MapPath("~/Content/Images/EpamTrainingCenterLogo.png");
                NewUserEmail(user.Email, user.Password, user.FirstName, htmlTemplateUrl, logoPath, epamTrainingCenterlogoPath);
                ModelState.Clear();
                var model = new User { SuccessMessage = "User Created Successfully." };
                return View("Create", model);
            }
            return View("Create");
        }
        public void NewUserEmail(string userName, string password, string name, string htmlTemplateUrl, string logoPath, string epamTrainingCenterlogoPath)
        {
            string body = string.Empty;
            using (var reader = new StreamReader(htmlTemplateUrl))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("@@User@@", name);
            body = body.Replace("@@UserName@@", userName);
            body = body.Replace("@@Password@@", password);
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(Environment.UserName + "@epam.com");
                mailMessage.Subject = "User Registration";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                var res = new LinkedResource(logoPath) { ContentId = "myImageID" };
                htmlView.LinkedResources.Add(res);
                mailMessage.AlternateViews.Add(htmlView);

                var res1 = new LinkedResource(epamTrainingCenterlogoPath) { ContentId = "epamTrainingCenterLogo" };
                htmlView.LinkedResources.Add(res1);
                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.To.Add(new MailAddress(userName));
                var obj = new Email
                {
                    ToEmail = userName,
                    FromEmail = Environment.UserName + "@epam.com",
                    Subject = "User Registration",
                    EmailContent = body,
                    CreatedDate = DateTime.Now,
                    Status = true
                };
                db.Emails.Add(obj);
                var smtp = new SmtpClient
                {
                    Host = ConfigurationManager.SMTPHost,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = CredentialCache.DefaultNetworkCredentials,
                    Port = Convert.ToInt32(ConfigurationManager.SMTPPort)
                };
                smtp.Send(mailMessage);
            }
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,RoleId,FirstName,MiddleName,LastName,GenderId,Email,ContactNumber,Address,CollegeId,MarksScored,GraduationSpecialty,EmailAddressToSendNotifications,Active,CreatedBy,CreatedDate,LastEditedBy,LastEditedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
