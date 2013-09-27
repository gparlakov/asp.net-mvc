using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies.ViewModels.ActorViewModels;

namespace Movies.Controllers
{
    public class ActorsController : Controller
    {
        private ApplicationDbContext dbContext;

        public ActorsController()
        {
            this.dbContext = new ApplicationDbContext();
        }

        //
        // Get Actors/All
        [ActionName("All")]
        public ActionResult GetAll()
        {
            var model = this.dbContext.Actors.Select(HWPersonVM.FromEntity);
            return View(model);
        }

        //
        // Get Actors/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // Post Actors/Create
        [HttpPost]
        public ActionResult Create(HWPersonVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateActor(model);
                    ViewBag.Info = "Created!";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            else
            {
                //var errors = ModelState.Select(m => m.
                ViewBag.Error = "Missing fields";
            }
            return View();
        }

        // 
        // Get Actors/Edit/1
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("All");
            }

            HWPersonVM model = GetActor(id);
            return View(model);
        }

        // 
        // Get Actors/Edit/1
        [HttpPost]
        public ActionResult Edit(HWPersonVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SaveActor(model);
                    ViewBag.Info = "Edited!";
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message = ex.InnerException.Message;
                    }
                    ViewBag.Error = message;
                }   
            }
            else
            {
                ViewBag.Error = "Invalid input.";
            }
            return View(model);
        }

        private void SaveActor(HWPersonVM model)
        {
            var dbModel = this.dbContext.Actors.Find(model.Id);
            dbModel.BirthDate = model.BirthDate;
            dbModel.Name = model.Name;
            dbModel.Sex = dbContext.Sexes.First(s => s.Name.ToLower() == model.Sex);
            dbContext.SaveChanges();
        }

        private HWPersonVM GetActor(int? id)
        {
            var actor = this.dbContext.Actors.Include("Sex").FirstOrDefault(a => a.Id == id);

            return new HWPersonVM 
            {
                Id = actor.Id,
                Name = actor.Name,
                Sex = actor.Sex.Name.ToLower(),
                BirthDate = actor.BirthDate != null ? 
                    actor.BirthDate.Value : 
                    DateTime.Now
            };
        }

        private void CreateActor(HWPersonVM model)
        {
            var actor = new Actor()
            {
                Name = model.Name,
                BirthDate = model.BirthDate,
                Sex = this.dbContext
                    .Sexes.First(mod => mod.Name == model.Sex)
            };

            this.dbContext.Actors.Add(actor);
            this.dbContext.SaveChanges();
        }
    }
}