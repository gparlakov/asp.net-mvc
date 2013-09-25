using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationalSite.Controllers
{
    public class MotoController : Controller
    {
        //
        // GET: /Moto/enduro
        [HttpGet]
        [ActionName("enduro")]
        public ActionResult GetEnduro()
        {
            return View();
        }

        //
        // GET: /Moto/sport
        [HttpGet]
        [ActionName("sport")]
        public ActionResult GetSport()
        {
            return View();
        }

        //
        // GET: /Moto/tourer
        [HttpGet]
        [ActionName("tourer")]
        public ActionResult GetTourer()
        {
            return View();
        }
	}
}