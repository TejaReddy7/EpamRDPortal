using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
