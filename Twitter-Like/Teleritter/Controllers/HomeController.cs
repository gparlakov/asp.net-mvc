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
        private IQueryable<Telereet> AllTelereets()
        {
            return this.data.Telreets.All()
                .OrderByDescending(t => t.PostedOn);
                
        }

        public ActionResult Index()
        {
            var telereets = this.AllTelereets().Take(6).Select(TelereetViewModel.FromTelereet);
            ViewBag.Message = "Last 6 telereets";
            return View(telereets);
        }

        public ActionResult Search(string text) 
        {
            if (text == null)
            {
                return Redirect("Index");
            }

            CacheSearch(text);

            ViewBag.Message = this.HttpContext.Cache[text + "message"].ToString();
            return View("Index", this.HttpContext.Cache[text]);
        }
  
        private void CacheSearch(string text)
        {
            if (this.HttpContext.Cache[text] == null)
            {
                var found = this.AllTelereets();
                found = found
                             .Where(t => t.Tags.Any(tag => tag.Name.Contains(text)));

                var viewModel = found.Select(TelereetViewModel.FromTelereet);

                var message = "Show by tag.|" + text + "| Cached at " + DateTime.Now;

                this.HttpContext.Cache.Add(text, viewModel, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
                this.HttpContext.Cache.Add(text + "message", "Cashed at " + DateTime.Now, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null); 
            }
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

        public ActionResult UserHomePage()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
	        {
		        throw new Exception("You are not allowed in here.");
	        }
            var usersTlereets = this.AllTelereets().Where(t => t.Author.Id == userId).Select(TelereetViewModel.FromTelereet);

            return View(usersTlereets);
        }

        public ActionResult ByTag(int? tagId)
        {
            if (tagId == null)
            {
                throw new Exception("Search by tag must contai tag to search by!");
            }

            var tagName = this.data.Tags.All().First(tag => tag.Id == tagId).Name;

            CacheSearch(tagName);

            ViewBag.Message = this.HttpContext.Cache[tagName + "message"];

            return View("Index", this.HttpContext.Cache[tagName]);
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
            if (tagsString == null)
            {
                return new List<Tag>();
            }

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