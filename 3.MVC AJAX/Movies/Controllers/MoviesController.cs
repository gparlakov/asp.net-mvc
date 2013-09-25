using Movies.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Movies.ViewModels;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext dbContext;

        public MoviesController()
        {
            this.dbContext = new ApplicationDbContext();
        }

        //
        // GET Movies/All
        [ActionName("All")]
        public ActionResult GetAll()
        {
            var movies = this.dbContext
                .Movies
                .Select(MovieVM.FromEntity);

            return View(movies);
        }

        //
        // GET Movies/Create
        [ActionName("Create")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateMovieVM
            {
                Actors = this.dbContext.Actors
                .Select(a => new SelectListItem 
                { 
                    Selected = false, 
                    Value = a.Id.ToString(), 
                    Text = a.Name 
                }),
                //Directors = this.dbContext.Directors.Select(PersonVM.FromEntity)
            };

            return View(model);
        }

        //
        // Post Movies/Create
        [ActionName("Create")]
        [HttpPost]
        public ActionResult Create(Movie model)
        {
            if (ModelState.IsValid)
            {
                this.dbContext.Movies.Add(model);
                try
                {
                    this.dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return View();
        }

	}
}