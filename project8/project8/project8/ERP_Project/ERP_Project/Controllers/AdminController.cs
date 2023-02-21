using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP_Project.Models;

namespace ERP_Project.Controllers
{
    [Authorize (Roles = "Admin")]
    public class AdminController : Controller
    {
        private ERP_ProjectEntities1 db = new ERP_ProjectEntities1();

        public ActionResult Dashboard()
        {

            int registeredCount = db.Students.Count();
            int regectCount = db.Students.Count(s => s.IsAccepted == false);
            int MajorsCount = db.Majors.Count();
            int acceptedCount = db.Students.Count(s => s.IsAccepted == true);
            int FacultiesCount = db.Faculties.Count();
            int PaymentsCount = db.RegisterClasses.Count(s => s.isPaid == true);

            ViewBag.RegectCount = regectCount;
            ViewBag.RegisteredCount = registeredCount;
            ViewBag.AcceptedCount = acceptedCount;
            ViewBag.MajorsCount = MajorsCount;
            ViewBag.FacultiesCount = FacultiesCount;
            ViewBag.PaymentsCount = PaymentsCount;


            return View();
        }

    }
}
