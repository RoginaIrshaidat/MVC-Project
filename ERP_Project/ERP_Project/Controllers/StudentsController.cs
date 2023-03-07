using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_Project.Models;
using Microsoft.AspNet.Identity;

namespace ERP_Project.Controllers
{
    //[Authorize(Roles = "Student")]

    public class StudentsController : Controller
    {
        private ERP_ProjectEntities db = new ERP_ProjectEntities();

        // GET: Students
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            //var id_student = db.Students.Find(userId);
            //Session["id"] = id_student.IdUser;
            var students = db.Students.Include(s => s.AspNetUser).Include(s => s.Major);
            return View(students.ToList());
        }
        public ActionResult Index2()
        {
            var students = db.Students.Include(s => s.AspNetUser).Include(s => s.Major);
            return View(students.ToList());
        }



        public ActionResult _profilelayout(int? id)
        {
            var students = db.Students.Include(s => s.StudentId == id);
            return View(students.ToList());
        }
        public ActionResult profile()
        {
            var loginID = User.Identity.GetUserId();
            var idss = db.Students.FirstOrDefault(a => a.IdUser == loginID).StudentId;
           var maj= db.Students.FirstOrDefault(a => a.IdUser == loginID).IdMajor;
            Session["major"]= maj;
            Session["userid"] = loginID;
            Session["studetId"]= idss;
            Student student = db.Students.Find(idss);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);

            //var students = db.Students.Where(s => s.StudentId == id).Include(s => s.Major);
            //return View(students.ToList());
        }
        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {

           ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Create([Bind(Include = "StudentId,IdUser,IdMajor,FirstName,LastName,BirthDate,Gender,PhoneNum,StudentImg,SchoolAvg,CertificateImg,PersonalIDImg,IsAccepted")] Student student)
        {
            string userId = User.Identity.GetUserId();
            student.IdUser = userId;
            student.IdMajor = Convert.ToInt32(Session["MajorIdSession"]);
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", student.IdUser);
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", student.IdMajor);
            return RedirectToAction("Index","Home");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", student.IdUser);
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", student.IdMajor);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,IdUser,IdMajor,FirstName,LastName,BirthDate,Gender,PhoneNum,StudentImg,SchoolAvg,CertificateImg,PersonalIDImg,IsAccepted")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", student.IdUser);
            ViewBag.IdMajor = new SelectList(db.Majors, "ID", "Name", student.IdMajor);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
