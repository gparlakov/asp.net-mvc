using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationalSite.Areas.Admin.Controllers
{
    public class SecretController : Controller
    {
        //
        // GET: /MotoSpecialRoute/123   // 3 digits
        [ActionName("number")]
        public ActionResult SecretNumber(string number)
        {
            ViewBag.number = number;
            return View("~/Areas/Admin/Views/Secret/Number.cshtml");
        }
	}
}