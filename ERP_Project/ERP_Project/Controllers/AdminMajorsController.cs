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

    public class AdminMajorsController : Controller
    {
        private ERP_ProjectEntities db = new ERP_ProjectEntities();

        // GET: AdminMajors
        public ActionResult Index()
        {
            var majors = db.Majors.Include(m => m.Faculty);
            return View(majors.ToList());
        }
        public ActionResult Search(string searchQuery)
        {
            var x1 = db.Majors.Include(m => m.Faculty).Where(x => x.Faculty.Name.Contains(searchQuery));
            return View("Index", x1.ToList());

        }

        // GET: AdminMajors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        // GET: AdminMajors/Create
        public ActionResult Create()
        {
            ViewBag.IdFaculty = new SelectList(db.Faculties, "ID", "Name");
            return View();
        }

        // POST: AdminMajors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,IdFaculty,HoursNum,Price,Description")] Major major)
        {
            if (ModelState.IsValid)
            {
                db.Majors.Add(major);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdFaculty = new SelectList(db.Faculties, "ID", "Name", major.IdFaculty);
            return View(major);
        }

        // GET: AdminMajors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdFaculty = new SelectList(db.Faculties, "ID", "Name", major.IdFaculty);
            return View(major);
        }

        // POST: AdminMajors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,IdFaculty,HoursNum,Price,Description")] Major major)
        {
            if (ModelState.IsValid)
            {
                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdFaculty = new SelectList(db.Faculties, "ID", "Name", major.IdFaculty);
            return View(major);
        }

        // GET: AdminMajors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        // POST: AdminMajors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Major major = db.Majors.Find(id);
            db.Majors.Remove(major);
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
