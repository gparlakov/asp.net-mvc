using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LibrarySystem.ViewModels.Books;
using LibrarySystem.ViewModels.Categories;

namespace LibrarySystem.Controllers
{
    public class BooksController : Controller
    {
        private LibrarySystemEntities db = new LibrarySystemEntities();

        // GET: /Books/
        public ActionResult Index()
        {
            ViewData["categories"] = db.Categories.Select(c => new CategoryVM { Id = c.Id, Name = c.Name }).ToList();
            //ViewData["defaulCategory"] = (ViewData["categories"] as IEnumerable<CategoryVM>).First();
            return View();
        }

        [HttpPost]
        public ActionResult CategoriesRead([DataSourceRequest]DataSourceRequest request)
        {
            var categories = db.Categories.Select(c => new CategoryVM { Id = c.Id, Name = c.Name }).ToList();

            return Json(categories.ToDataSourceResult(request));
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var books = db.Books
                .Include(b => b.Category)
                .Select(BookGridVM.FromBook);

            return Json(books.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Update(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    book.CategoryId = book.Category.Id;
                    book.Category = db.Categories.Find(book.Category.Id);
                    db.Entry<Book>(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ff", ex);

                    return Json(new {Error = ex.Message});
                }
            }

            return Json(new {Error = "Invalid!"});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
