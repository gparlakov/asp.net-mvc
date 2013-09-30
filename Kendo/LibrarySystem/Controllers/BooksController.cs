using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            return View();
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var books = db.Books
                .Include(b => b.Category)
                .Select(BookGridVM.FromBook);

            return Json(books.ToDataSourceResult(request));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Book book,
            [DataSourceRequest]DataSourceRequest request)
        {
            if (book != null && ModelState.IsValid)
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
                    var validationError = ex as DbEntityValidationException;
                    if (validationError != null)
                    {
                        foreach (var item in validationError.EntityValidationErrors.First().ValidationErrors)
                        {
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
                    }
                }
            }

            return Json((new[] { book }).ToDataSourceResult(request, ModelState));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Insert(Book book, [DataSourceRequest]DataSourceRequest request)
        {
            if (book!= null && ModelState.IsValid)
            {
                try
                {
                    book.CategoryId = book.Category.Id;
                    book.Category = db.Categories.Find(book.CategoryId);
                    this.db.Books.Add(book);
                    db.SaveChanges();
                    return Json(new { });
                }
                catch (Exception ex)
                {
                    var validationError = ex as DbEntityValidationException;
                    if (validationError != null)
	                {
                        foreach (var item in validationError.EntityValidationErrors.First().ValidationErrors)
                        {
                            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                        }
	                }

                    return Json(ModelState.ToDataSourceResult());
                }
            }
            
            return Json((new[] {""}).ToDataSourceResult(request, ModelState));
        }

        public ActionResult Delete(int? id, [DataSourceRequest]DataSourceRequest request)
        {
            if (id == null)
            {
                return new EmptyResult();
            }
            var bookToDelete = this.db.Books.Find(id);
            try
            {
                this.db.Books.Remove(bookToDelete);
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var validationError = ex;
                if (validationError != null)
                {
                    foreach (var item in validationError.EntityValidationErrors.First().ValidationErrors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }

            return Json(new[] { "" }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
