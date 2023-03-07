using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_Project.Models;

namespace ERP_Project.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class AdminRegisterClassesController : Controller
    {
        private ERP_ProjectEntities db = new ERP_ProjectEntities();

        // GET: RegisterClasses
        public ActionResult Index()
        {
            var registerClasses = db.RegisterClasses.Include(r => r.Section).Include(r => r.Student);
            return View(registerClasses.ToList());
        }


        // GET: RegisterClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterClass registerClass = db.RegisterClasses.Find(id);
            if (registerClass == null)
            {
                return HttpNotFound();
            }
            return View(registerClass);
        }

        // GET: RegisterClasses/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Sections, "ClassID", "ClassNumber");
            ViewBag.student_id = new SelectList(db.Students, "StudentId", "IdUser");
            return View();
        }

        // POST: RegisterClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "student_id,ClassID,isPaid,registerclasses")] RegisterClass registerClass)
        {
            if (ModelState.IsValid)
            {
                db.RegisterClasses.Add(registerClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Sections, "ClassID", "ClassNumber", registerClass.ClassID);
            ViewBag.student_id = new SelectList(db.Students, "StudentId", "IdUser", registerClass.student_id);
            return View(registerClass);
        }

        // GET: RegisterClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterClass registerClass = db.RegisterClasses.Find(id);
            if (registerClass == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Sections, "ClassID", "ClassNumber", registerClass.ClassID);
            ViewBag.student_id = new SelectList(db.Students, "StudentId", "IdUser", registerClass.student_id);
            return View(registerClass);
        }

        // POST: RegisterClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "student_id,ClassID,isPaid,registerclasses")] RegisterClass registerClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Sections, "ClassID", "ClassNumber", registerClass.ClassID);
            ViewBag.student_id = new SelectList(db.Students, "StudentId", "IdUser", registerClass.student_id);
            return View(registerClass);
        }

        // GET: RegisterClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterClass registerClass = db.RegisterClasses.Find(id);
            if (registerClass == null)
            {
                return HttpNotFound();
            }
            return View(registerClass);
        }

        // POST: RegisterClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegisterClass registerClass = db.RegisterClasses.Find(id);
            db.RegisterClasses.Remove(registerClass);
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
