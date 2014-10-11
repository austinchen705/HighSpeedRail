using HighSpeedRail.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HighSpeedRail.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }
    }
}
