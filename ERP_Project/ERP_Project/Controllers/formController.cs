using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_Project.Controllers
{
    [AllowAnonymous]
    public class formController : Controller
    {
        // GET: form
        public ActionResult Index()
        {
            return View();
        }
    }
}