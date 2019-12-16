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
    public class EmailRecipientsController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: EmailRecipients
        public ActionResult Index()
        {
            return View(db.EmailRecipients.ToList());
        }

        // GET: EmailRecipients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailRecipient emailRecipient = db.EmailRecipients.Find(id);
            if (emailRecipient == null)
            {
                return HttpNotFound();
            }
            return View(emailRecipient);
        }

        // GET: EmailRecipients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailRecipients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailRecipientID,FirstName,LastName,Email,CollegeId,Active")] EmailRecipient emailRecipient)
        {
            if (ModelState.IsValid)
            {
                db.EmailRecipients.Add(emailRecipient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailRecipient);
        }

        // GET: EmailRecipients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailRecipient emailRecipient = db.EmailRecipients.Find(id);
            if (emailRecipient == null)
            {
                return HttpNotFound();
            }
            return View(emailRecipient);
        }

        // POST: EmailRecipients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailRecipientID,FirstName,LastName,Email,CollegeId,Active")] EmailRecipient emailRecipient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailRecipient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailRecipient);
        }

        // GET: EmailRecipients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailRecipient emailRecipient = db.EmailRecipients.Find(id);
            if (emailRecipient == null)
            {
                return HttpNotFound();
            }
            return View(emailRecipient);
        }

        // POST: EmailRecipients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailRecipient emailRecipient = db.EmailRecipients.Find(id);
            db.EmailRecipients.Remove(emailRecipient);
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
