using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data;
using App.Data.Interfaces;

namespace App.Controllers
{
    public class BaseController : Controller
    {
        //public BaseController()
        //    :this(new UowData())
        //{
        //}

        public BaseController(IUowData data)
        {
            this.Data = data;
        }

        protected IUowData Data { get; set; }
    }
}