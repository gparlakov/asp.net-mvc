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
            var calculateModel = GetNormalValue(model);
            return PartialView("PartialCalculate", calculateModel);
        }

        private CalculateModel GetNormalValue(RequestModel model)
        {
            Units unit = (Units)int.Parse(model.Unit.Substring(0,1));
            var bit = int.Parse(model.Unit.Substring(1, 1));
            double value = model.Number* Math.Pow(model.Kilo, (int)unit) * bit;

            return new CalculateModel 
            { 
                NormalizedValue = value, 
                Unit = unit,
                Kilo = model.Kilo
            };
        }
    }
}