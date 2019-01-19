using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceptionProcam.Areas.Area.Controllers
{
    public class WelcomeController : Controller
    {
        // GET: Area/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}