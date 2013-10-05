using System;
using System.Linq;
using System.Web.Mvc;
using Teleritter.Models;
using Twitter.Controllers;
using Twitter.Models;
using Microsoft.AspNet.Identity;

namespace Teleritter.Controllers
{
    public class HomeController : BaseController
    {
        private IQueryable<TelereetViewModel> AllTelereets()
        {
            return this.data.Telreets.All()
                .OrderByDescending(t => t.PostedOn)
                .Select(t => new TelereetViewModel
                {
                    Author = t.Author.UserName,
                    Text = t.Text,
                    Tags = t.Tags.AsQueryable().Select(TagViewModel.FromTag),
                    PostedOn = t.PostedOn
                });
        }

        public ActionResult Index()
        {
            var telereets = this.AllTelereets().Take(6);
            ViewBag.Message = "Last 6 telereets";
            return View(telereets);
        }

        public ActionResult Search(string text) 
        {
            if (text == null)
            {
                return Redirect("Index");
            }

            if (this.HttpContext.Cache[text] == null)
            {
                var found = this.AllTelereets();
                found = found
                    .Where(t => t.Tags.Any(tag => tag.Name.Contains(text)));

                this.HttpContext.Cache.Add(text, found, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                this.HttpContext.Cache.Add(text + "message", "Cashed at " + DateTime.Now, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null); 
            }

            ViewBag.Message = "Telereets tagged #" + text + " " + this.HttpContext.Cache[text + "message"].ToString();
            return View("Index", this.HttpContext.Cache[text]);
        }

        public ActionResult Post(string newPostText)
        {
            var newPost = new Telereet 
            {
                PostedOn= DateTime.Now,                
                Author = this.data.Users.All().FirstOrDefault(u => u.Id == User.Identity.GetUserId()),
                Text = newPostText,

            };

        }
    }
}