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
    [Authorize(Roles = "Admin")]

    public class AdminSubjectsController : Controller
    {
        private ERP_ProjectEntities1 db = new ERP_ProjectEntities1();

        // GET: AdminSubjects
        public ActionResult Index()
        {
            var subjects = db.Subjects.Include(s => s.Major);
            return View(subjects.ToList());
        }
        public ActionResult Search(string searchQuery)
        {
            var x1 = db.Subjects.Include(s => s.Major).Where(x => x.Major.Name.Contains(searchQuery));
            return View("Index", x1.ToList());

        }
        // GET: AdminSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: AdminSubjects/Create
        public ActionResult Create()
        {
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name");
            return View();
        }

        // POST: AdminSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Subject,Subject_Name,IdMajor,SubjectHours,SubjectCode")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", subject.IdMajor);
            return View(subject);
        }

        // GET: AdminSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", subject.IdMajor);
            return View(subject);
        }

        // POST: AdminSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Subject,Subject_Name,IdMajor,SubjectHours,SubjectCode")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", subject.IdMajor);
            return View(subject);
        }

        // GET: AdminSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: AdminSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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
