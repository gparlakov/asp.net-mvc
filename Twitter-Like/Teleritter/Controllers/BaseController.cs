using System;
using System.Linq;
using System.Web.Mvc;
using Teleritter.Data;
using Teleritter.Data.Interfaces;

namespace Twitter.Controllers
{
    public class BaseController : Controller
    {
        protected IUowData data;

        public BaseController(IUowData data)
        {
            this.data = data;
        }

        public BaseController()
            :this(new UowData())
        {
        }
    }
}