using MVCTest.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calculate(RequestModel model)
        {
            return PartialView("_CalculatePartialPage", model);
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
    }
}