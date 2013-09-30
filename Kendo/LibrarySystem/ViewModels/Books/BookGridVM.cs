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
        public int Id { get; set; }

        [Required,StringLength(100, ErrorMessage="Title length must be 2 to 100.", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [StringLength(17)]
        public string ISBN { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

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