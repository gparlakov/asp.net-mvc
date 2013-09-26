//using Movies.Models;
//using System;
//using System.Linq;
//using System.Web.Mvc;
//using Movies.ViewModels;
//using Movies.ViewModels.MovieViewModels;
//using System.Data.Entity.Core;

//namespace Movies.Controllers
//{
//    public class MoviesControllercopy : Controller
//    {
//        private ApplicationDbContext dbContext;

//        public MoviesController()
//        {
//            this.dbContext = new ApplicationDbContext();
//        }

//        //
//        // GET Movies/All
//        [ActionName("All")]
//        public ActionResult GetAll()
//        {
//            var movies = this.dbContext
//                .Movies
//                .Select(MovieVM.FromEntity);

//            return View(movies);
//        }

//        //
//        // GET Movies/Create
//        [ActionName("Create")]
//        [HttpGet]
//        public ActionResult Create()
//        {
//            var model = GetCreateViewModel();

//            return View(model);
//        }

//        //
//        // Post Movies/Create
//        [ActionName("Create")]
//        [HttpPost]
//        public ActionResult Create(CreateMovieVM model)
//        {
//            if (ModelState.IsValid)
//            {
//                this.dbContext.Movies.Add(new Movie() 
//                {
//                   Title = model.Title,
//                   Year = model.Year,
//                   LeadingActor = this.dbContext.Actors.Find(model.LeadingActorId),
//                   LeadingActress = this.dbContext.Actors.Find(model.LeadingActressId),
//                   Director = this.dbContext.Directors.Find(model.DirectorId)
//                });
//                try
//                {
//                    this.dbContext.SaveChanges();
//                    ViewBag.Info = "Created!";
//                }
//                catch(Exception ex)
//                {
//                    var message = ex.Message;
//                    if (ex.InnerException != null)
//                    {
//                        message = ex.InnerException.Message;
//                    }
//                    ViewBag.Error = message;
//                }
//            }
//            return this.Create();
//        }

//        //
//        // Get Movies/Edit
//        [ActionName("Edit")]
//        [HttpGet]
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//               return Redirect("~/Movies/All");
//            }
//            var movie = GetMovieEditModel(id);

//            return View(movie);
//        }

//        //
//        // Post Movies/Edit/1
//        [ActionName("Edit")]
//        [HttpPost]
//        public ActionResult Edit(EditMovieVM model)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    SaveEditedMovie(model);

//                    ViewBag.Info = "Edited!";
//                }
//                catch(Exception ex)
//                {
//                    var message = ex.Message;
//                    if (ex.InnerException != null)
//                    {
//                        message = ex.InnerException.Message;
//                    }
//                    ViewBag.Error = message;
//                }
//            }
//            else
//            {
//                ViewBag.Error = "Could not save to DB.";
//            }

//            return Redirect("~/Movies/All");
//        }

//        // 
//        // Get Movies/Details
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return Redirect("~/Movies/All");
//            }

//            var movie = GetDetails(id);
//            return View(movie);
//        }

//        //
//        // Delete Movies/Delete/4
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return Redirect("~/Movies/All");
//            }
//            try
//            {
//                DeleteMovie(id);
//                ViewBag.Info = "Deleted!";
//            }
//            catch(EntityException ex)
//            {
//                var message = ex.Message;
//                if (ex.InnerException != null)
//                {
//                    message = ex.InnerException.Message;
//                }
//                ViewBag.Error = message;
//            }

//            return RedirectToAction("All");
//        }

//        #region Private methods
//        private MovieDetailsVM GetDetails(int? id)
//        {
//            var dbMovie = GetMovieFullInfo(id);

//            return new MovieDetailsVM
//            {
//                Title = dbMovie.Title,
//                Year = dbMovie.Year,
//                LeadingActor = dbMovie.LeadingActor != null ?
//                    dbMovie.LeadingActor.Name :
//                    null,
//                LeadingActress = dbMovie.LeadingActress != null ?
//                    dbMovie.LeadingActress.Name :
//                    null,
//                Director = dbMovie.Director != null ?
//                    dbMovie.Director.Name :
//                    null,
//                Studio = dbMovie.Studio != null ?
//                    dbMovie.Studio.Name :
//                    null
//            };
//        }

//        private Movie GetMovieFullInfo(int? id)
//        {
//            var movie = (Movie)this.dbContext.Movies
//                .Include("LeadingActor")
//                .Include("LeadingActress")
//                .Include("Director")
//                .Include("Studio")
//                .FirstOrDefault(m => m.Id == id);

//            return movie;
//        }

//        private void SaveEditedMovie(EditMovieVM model)
//        {
//            var movie = this.dbContext.Movies.Find(model.Id);
//            movie.Title = model.Title;
//            movie.Year = model.Year;

//            movie.LeadingActor = model.LeadingActorId != null ?
//                this.dbContext.Actors.Find(model.LeadingActorId) :
//                null;

//            movie.LeadingActress = model.LeadingActressId != null ?
//                this.dbContext.Actors.Find(model.LeadingActressId) :
//                null;

//            movie.Director = model.DirectorId != null ?
//                this.dbContext.Directors.Find(model.DirectorId) :
//                null;

//            this.dbContext.SaveChanges();
//        }

//        private CreateMovieVM GetCreateViewModel()
//        {
//            var model = new CreateMovieVM
//            {
//                Actors = this.dbContext.Actors
//                    .Where(a => a.Sex.Name == "Male")
//                    .Select(PersonVM.FromEntity).ToList(),
//                Actresses = this.dbContext.Actors
//                    .Where(a => a.Sex.Name == "Female")
//                    .Select(PersonVM.FromEntity).ToList(),
//                Directors = this.dbContext.Directors.Select(PersonVM.FromEntity).ToList()
//            };
//            return model;
//        }

//        private EditMovieVM GetMovieEditModel(int? id)
//        {
//            var dbMovie = GetMovieFullInfo(id);

//            var movie = new EditMovieVM()
//            {
//                Id = dbMovie.Id,
//                Title = dbMovie.Title,
//                Year = dbMovie.Year
//            };

//            movie.Actors = this.dbContext.Actors
//                    .Where(a => a.Sex.Name == "Male")
//                    .Select(PersonVM.FromEntity).ToList();
//            movie.Actresses = this.dbContext.Actors
//                    .Where(a => a.Sex.Name == "Female")
//                    .Select(PersonVM.FromEntity).ToList();
//            movie.Directors = this.dbContext.Directors.Select(PersonVM.FromEntity).ToList();

//            if (dbMovie.LeadingActor != null)
//            {
//                movie.SelectedActorId = dbMovie.LeadingActor.Id;
//            }

//            if (dbMovie.LeadingActress != null)
//            {
//                movie.SelectedActressId = dbMovie.LeadingActress.Id;
//            }
//            if (dbMovie.Director != null)
//            {
//                movie.DirectorId = dbMovie.Director.Id;
//            }
//            return movie;
//        }

//        private void DeleteMovie(int? id)
//        {
//            this.dbContext.Movies.Remove(this.dbContext.Movies.Find(id));
//            this.dbContext.SaveChanges();
//        }
//        #endregion
//    }
//}