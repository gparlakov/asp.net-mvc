using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.ViewModels.Books;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private Models.LibrarySystemEntities data;

        public HomeController()
        {
            this.data = new LibrarySystem.Models.LibrarySystemEntities();
        }

        public ActionResult Index()
        {
            var booksInCategories = this.GetBooksInCategories();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private IEnumerable<BooksInCategoriesVM> GetBooksInCategories()
        {
            var booksInCategories = this.data.Categories
                .Include("Books")
                .Select(cat => new BooksInCategoriesVM
                {
                    Name = cat.Name,
                    Books = cat.Books.Select(b => new BookVM
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author
                    })
                })
                .OrderBy(c => c.Name)
                .ToList();

            return booksInCategories;
        }

    }
}