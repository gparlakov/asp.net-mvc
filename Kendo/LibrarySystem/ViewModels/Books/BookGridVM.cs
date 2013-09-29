using LibrarySystem.Models;
using LibrarySystem.ViewModels.Categories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
namespace LibrarySystem.ViewModels.Books
{
    public class BookGridVM
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [AllowHtml]
        public string Title { get; set; }

        [AllowHtml]
        public string Author { get; set; }

        [AllowHtml]
        public string ISBN { get; set; }

        [AllowHtml]
        public string Url { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public CategoryVM Category { get; set; }

        public static Expression<Func<Book, BookGridVM>> FromBook
        {
            get
            {
                return b => new BookGridVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    Url = b.Url,
                    Description = b.Description,
                    Category = new CategoryVM
                    {
                        Name = b.Category.Name,
                        Id = b.Category.Id
                    }
                };
            }
        }
    }
}