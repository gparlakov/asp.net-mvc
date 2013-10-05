using System;
using System.Linq;
using System.Web.Mvc;
using Teleritter.Models;
using Teleritter.Controllers;
using Teleritter.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;

namespace Teleritter.Controllers
{
    [ValidateInput(false)]    
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

        public ActionResult PostNewTelereet(NewTelereetViewModel model)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                if (string.IsNullOrEmpty(userId))
	            {
		            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(", ", "User must be logged for new Telereet")); 
	            }
                if (ModelState.IsValid)
                {
                    var newPost = CreateTelereet(userId, model);

                    this.data.Telreets.Add(newPost);
                    this.data.SaveChanges();

                    var newTelereet = CreateTelereetViewModel(newPost);

                    return PartialView("_TelereetPartial", newTelereet);
                }
                else
                {
                    var errorsMessages = ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Join(", ", errorsMessages)); 
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Database connection lost");
            }
        }
  
        private TelereetViewModel CreateTelereetViewModel(Telereet newPost)
        {
            var newTelereet = new TelereetViewModel
            {
                Author = newPost.Author.UserName,
                Text = newPost.Text,
                PostedOn = newPost.PostedOn,
                Tags = newPost.Tags.AsQueryable().Select(TagViewModel.FromTag),
            };
            return newTelereet;
        }
  
        private Telereet CreateTelereet(string userId, NewTelereetViewModel model)
        {
            var newPost = new Telereet
            {
                PostedOn = DateTime.Now,
                Author = this.data.Users.All().FirstOrDefault(u => u.Id == userId),
                Text = model.Text,
                Tags = GetTags(model.Tags).ToList()
            };
            return newPost;
        }

        private IEnumerable<Tag> GetTags(string tagsString)
        {
            var tags = tagsString
                .Trim()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new Tag{Name = s});

            var tagsInBase = new List<Tag>();

            foreach (var tag in tags)
            {
                var tagInBase = this.data.Tags.All()
                    .FirstOrDefault(t => t.Name == tag.Name);

                if (tagInBase == null)
                {
                    tagInBase = tag;
                }

                tagsInBase.Add(tagInBase);
            }

            return tagsInBase;
        }
    }
}