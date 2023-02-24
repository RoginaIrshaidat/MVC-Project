using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using ERP_Project.Models;
using Microsoft.AspNet.Identity;

namespace ERP_Project.Controllers
{
    [Authorize(Roles = "Student")]

    public class SectionsController : Controller
    {
        private ERP_ProjectEntities1 db = new ERP_ProjectEntities1();

        // GET: Sections

        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.RegisterClasses).Include(s => s.Subject);

            return View(sections.ToList());
        }
        public ActionResult Index33(int? id)
        {
            var sections = db.Sections.Where(s => s.IdSubject==id);
            return View(sections.ToList());
        }
       
      


        public ActionResult Add(int? id)
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


        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddConfirmed(int id)
        {
            var loginID = User.Identity.GetUserId();
           var idss= db.Students.FirstOrDefault(a => a.IdUser == loginID).StudentId;
            Session["StudentIdSession"] = idss;

            bool flag = true;
            int subId = db.Sections.Find(id).ClassID;

            foreach (var x in db.RegisterClasses.ToList())
            {
                if (idss == x.student_id && subId == x.ClassID)
                {                
                    flag = false;
                    break;
                }
            }
            if (flag == false)
            {
                TempData["swal_message"] = $"Already you add this section ";
                ViewBag.title = "Error";
                ViewBag.icon = "error";
            }

            bool timeCheck = true;
            if (flag == true)
            {
                var time = (from startTime in db.Sections where startTime.ClassID == id select new { startTime.StartTime }).FirstOrDefault();
                foreach (var x in db.RegisterClasses.ToList())
                {
                    if (x.Section.StartTime == time.StartTime )
                    {
                        timeCheck = false;
                        break;
                    }                  
                    
                }
                if (timeCheck == true)
                {
                    RegisterClass register = new RegisterClass();
                    register.student_id = idss;
                    register.ClassID = id;

                    db.RegisterClasses.Add(register);
                    db.SaveChanges();
                }
                else
                {
                    TempData["swal_message"] = $" you have class in the same time ";
                    ViewBag.title = "Error";
                    ViewBag.icon = "error";
                    
                }
            }
            return RedirectToAction("myClasses", "Sections", Session["StudentIdSession"]);

        }
        public ActionResult myClasses(int? id)
        {
            var loginID = User.Identity.GetUserId();
            var idss = db.Students.FirstOrDefault(a => a.IdUser == loginID).StudentId;

            var sections = db.RegisterClasses.Where(s => s.student_id==idss ).Include(s => s.Section).ToList();

            return View(sections);
        }
        // GET: Sections/Details/5
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

        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.IdSubject = new SelectList(db.Subjects, "ID_Subject", "Subject_Name");
            return View();
        }

        // POST: Sections/Create
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

        // GET: Sections/Edit/5
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

        // POST: Sections/Edit/5
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

        // GET: Sections/Delete/5
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

        // POST: Sections/Delete/5
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
