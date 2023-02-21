using ERP_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_Project.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {
        ERP_ProjectEntities1 db = new ERP_ProjectEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult _userProfile()
        {
            int id =Convert.ToInt32(Session["userid"]);
            var x = db.Students.Find(id);
            return PartialView("_userProfile",x);
        }
    }
}