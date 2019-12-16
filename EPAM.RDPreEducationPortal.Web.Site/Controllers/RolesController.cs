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
    public class RolesController : Controller
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();

        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Master_Roles.ToList());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Roles master_Roles = db.Master_Roles.Find(id);
            if (master_Roles == null)
            {
                return HttpNotFound();
            }
            return View(master_Roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleId,RoleName,RoleKey")] Master_Roles master_Roles)
        {
            if (ModelState.IsValid)
            {
                db.Master_Roles.Add(master_Roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(master_Roles);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Roles master_Roles = db.Master_Roles.Find(id);
            if (master_Roles == null)
            {
                return HttpNotFound();
            }
            return View(master_Roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoleId,RoleName,RoleKey")] Master_Roles master_Roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(master_Roles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(master_Roles);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Master_Roles master_Roles = db.Master_Roles.Find(id);
            if (master_Roles == null)
            {
                return HttpNotFound();
            }
            return View(master_Roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Master_Roles master_Roles = db.Master_Roles.Find(id);
            db.Master_Roles.Remove(master_Roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Roles/List
        public JsonResult List()
        {
            return Json(db.Master_Roles.ToList(), JsonRequestBehavior.AllowGet);
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
