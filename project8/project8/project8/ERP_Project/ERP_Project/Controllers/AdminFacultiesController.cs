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

    public class AdminFacultiesController : Controller
    {
        private ERP_ProjectEntities1 db = new ERP_ProjectEntities1();

        // GET: Faculties
        public ActionResult Index()
        {
            return View(db.Faculties.ToList());
        }
        public ActionResult Search( string searchQuery)
        {

            var x1 = db.Faculties.Where(x => x.Name.Contains(searchQuery));
            return View("Index", x1.ToList());

        }

        // GET: Faculties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // GET: Faculties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Faculty faculty, HttpPostedFileBase img)
        {
            if (img != null)
            {
                string path = Server.MapPath("../Images/") + img.FileName;
                img.SaveAs(path);
                faculty.ImgFaculty = img.FileName;
            }


            db.Faculties.Add(faculty);
            db.SaveChanges();
            return RedirectToAction("Index", "AdminFaculties");


        }

        // GET: Faculties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");

        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Faculty faculty, int ID, HttpPostedFileBase img)
        {
            Faculty emp = db.Faculties.Find(ID);
            emp.Description = Request["Description"];
            emp.Name = Request["Name"];
            emp.AcceptanceRate = Convert.ToInt32(Request["AcceptanceRate"]);


            if (img != null)
            {
                string path = Server.MapPath("../../Images/") + img.FileName;
                img.SaveAs(path);
                emp.ImgFaculty = img.FileName;
            }
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Faculties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return HttpNotFound();
            }
            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Faculty faculty = db.Faculties.Find(id);
            db.Faculties.Remove(faculty);
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
