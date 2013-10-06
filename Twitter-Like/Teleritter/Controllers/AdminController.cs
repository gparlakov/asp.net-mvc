using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Teleritter.Models;

namespace Teleritter.Controllers
{
    public class AdminController : BaseController
    {
        private const int PageSize = 10;

        //
        // GET: /Admin/
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
		        id = 1;
            }

            var telereets = this.data.Telreets.All()
                .Select(TelereetAdminViewModel.FromTelereet)
                .OrderByDescending(t => t.PostedOn);

            ViewBag.TotalPages = Math.Ceiling((double)telereets.Count() / PageSize);


            return View(telereets.Skip(id.Value - 1).Take(PageSize));
        }

        //
        // GET: /Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create
        public ActionResult Create()
        {
            var newModel = new TelereetAdminViewModel();
            return View(newModel);
        }

        //
        // POST: /Admin/Create
        [HttpPost]
        public ActionResult Create(TelereetAdminViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //model.Tags = JsonConvert.DeserializeObject<IEnumerable<string>>(model.Tags);

                    var newTelereet = new Telereet 
                    {
                        Author = this.data.Users.All().Where(u => u.Id == model.Author).First(),
                        Text = model.Text,
                        PostedOn = model.PostedOn,
                        Tags = GetTagsInDB(model.Tags).ToList()
                    };

                    this.data.Telreets.Add(newTelereet);
                    this.data.SaveChanges();

                    ViewBag.Message = "Created!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Admin/Edit/5
        public ActionResult Edit(int id)
        {
            var telereet = this.data.Telreets
                .All()
                .Select(TelereetAdminViewModel.FromTelereet)
                .FirstOrDefault(t => t.Id == id);

            return View(telereet);
        }

        //
        // POST: /Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetAllUsers()
        {
            try 
	        {
                var users = this.data.Users.All().Select(UserAdminViewModel.FromUser);

                return Json(users, JsonRequestBehavior.AllowGet);
	        }
	        catch (Exception)
	        {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "No tags could be read from database");
	        }
            
        }
        
        public ActionResult GetAllTags()
        {
            try 
	        {
                var tags = this.data.Tags.All().Select(TagViewModel.FromTag);

                return Json(tags, JsonRequestBehavior.AllowGet);
	        }
	        catch (Exception)
	        {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "No tags could be read from database");
	        }
        }

        private IEnumerable<Tag> GetTagsInDB(IEnumerable<string> tagsCollection)
        {
            var tags = tagsCollection
                .Select(t => new Tag { Name = t });

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
