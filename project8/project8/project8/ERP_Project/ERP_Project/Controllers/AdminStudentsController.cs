using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ERP_Project.Models;

namespace ERP_Project.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminStudentsController : Controller
    {
        private ERP_ProjectEntities1 db = new ERP_ProjectEntities1();

        // GET: Students
        public ActionResult Requsts()
        {
            {
                var students = db.Students.Where(e => e.IsAccepted == null).Include(s => s.Major);
                if (students.ToList().Count == 0)
                {
                    ViewBag.result = "No Request Right Now!";
                }
                return View(students.ToList());
            }
        }

        public ActionResult AcceptedStudents()
        {
            {

                var students = db.Students.Where(s => s.IsAccepted == true).Include(s => s.Major);
                return View(students.ToList());
            }
        }
        public ActionResult RejectedStudents()
        {

            {
                var students = db.Students.Where(s => s.IsAccepted == false).Include(s => s.Major);
                return View(students.ToList());
            }
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
        public ActionResult Edit(Student student, int id, HttpPostedFileBase img)
        {

            //int id = Convert.ToInt32(Request["StudentId"]); 
            Student emp = db.Students.Find(id);
            emp.IsAccepted = student.IsAccepted;


            if (img != null)
            {
                string path = Server.MapPath("~/Images/") + img.FileName;
                img.SaveAs(path);
                student.StudentImg = img.FileName;
            }

            if (ModelState.IsValid)
            {
                //db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                if (student.IsAccepted == true)
                {
                    //MailMessage mail = new MailMessage();
                    ////mail.To.Add(db.Students.Find(id).Email);
                    //mail.From = new MailAddress("hopeorganization23@gmail.com");
                    //mail.Subject = "Hello test email";
                    //mail.Body = $"Wellcome {student.FirstName} ";
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 587; // 25 465
                    //smtp.EnableSsl = true;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Credentials = new System.Net.NetworkCredential("hopeorganization23@gmail.com", "ggdtgqgdkjiecrwq");
                    //smtp.Send(mail);
                }
                else
                {
                    //MailMessage mail = new MailMessage();
                    ////mail.To.Add(student.Email);
                    //mail.From = new MailAddress("hopeorganization23@gmail.com");
                    //mail.Subject = "Hello test email";
                    //mail.Body = $"Wellcome {db.Students.Find(id).FirstName} ";
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 587; // 25 465
                    //smtp.EnableSsl = true;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Credentials = new System.Net.NetworkCredential("hopeorganization23@gmail.com", "ggdtgqgdkjiecrwq");
                    //smtp.Send(mail);
                }
                return RedirectToAction("Requsts");
            }

            return View(student);
        }


    }
}
