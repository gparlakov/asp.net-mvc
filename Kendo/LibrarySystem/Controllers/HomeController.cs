﻿using System;
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
            return View(booksInCategories);
        }

        public ActionResult Search(string query)
        {
            var found = this.SearchFor(query);
            ViewBag.Query = query;
            return View(found);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var book = this.data.Books
                .Include("Category")
                .FirstOrDefault(b => b.Id == id);

            return View(book);
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

        private IEnumerable<BookVM> SearchFor(string text)
        {
            var found = this.data.Books
                .Include("Category")
                .Where(b => b.Title.Contains(text) || b.Author.Contains(text))
                .Select(b => new BookVM 
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Category = b.Category.Name
                });

            return found;
        }

    }
}