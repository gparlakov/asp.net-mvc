using App.Data.Interfaces;
using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace App.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUowData data)
            :base(data)
        {
        }

        public ActionResult Index()
        {
            var allLaptops = this.Data.Laptops.All()
                .OrderByDescending(l => l.Votes.Count)
                .Take(6)
                .Select(LaptopViewModel.FromLaptop);

            return View(allLaptops);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return Redirect("Index");
            }

            var laptop = this.Data.Laptops.All()
                .Select(LaptopDetailsViewModel.FromLaptop)
                .FirstOrDefault(l => l.Id == id);

            return View(laptop);
        }

        public ActionResult PostComment(PostCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    AuthorId = User.Identity.GetUserId(),
                    Text = model.Comment,
                    LaptopId = model.LaptopId
                };

                try
                {
                    this.Data.Comments.Add(comment);
                    this.Data.SaveChanges();

                    return Content("<div>" + model.Comment + "</div>");
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError, "Could not save comment to DB");
                }
            }
            else
            {
                var errors = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors.Select(e =>e.ErrorMessage));


                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, string.Join(", ", errors));
            }
        }
    }
}