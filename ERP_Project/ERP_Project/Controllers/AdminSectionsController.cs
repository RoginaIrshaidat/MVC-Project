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

    public class AdminSectionsController : Controller
    {
        private ERP_ProjectEntities db = new ERP_ProjectEntities();

        // GET: AdminSections
        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.Subject);
            return View(sections.ToList());
        }
        public ActionResult Search(string searchQuery)
        {
            var x1 = db.Sections.Include(s => s.Subject).Where(x => x.Subject.Subject_Name.Contains(searchQuery));
            return View("Index", x1.ToList());

        }
        // GET: AdminSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: AdminSections/Create
        public ActionResult Create()
        {
            ViewBag.IdSubject = new SelectList(db.Subjects, "ID_Subject", "Subject_Name");
            return View();
        }

        // POST: AdminSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,ClassNumber,IdSubject,Days,StartTime,EndTime,ClassStatus,Instructor,Location")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSubject = new SelectList(db.Subjects, "ID_Subject", "Subject_Name", section.IdSubject);
            return View(section);
        }

        // GET: AdminSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSubject = new SelectList(db.Subjects, "ID_Subject", "Subject_Name", section.IdSubject);
            return View(section);
        }

        // POST: AdminSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,ClassNumber,IdSubject,Days,StartTime,EndTime,ClassStatus,Instructor,Location")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSubject = new SelectList(db.Subjects, "ID_Subject", "Subject_Name", section.IdSubject);
            return View(section);
        }

        // GET: AdminSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: AdminSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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
