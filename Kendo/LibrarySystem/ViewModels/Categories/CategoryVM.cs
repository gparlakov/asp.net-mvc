using LibrarySystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace LibrarySystem.ViewModels.Categories
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        public static Expression<Func<Category, CategoryVM>> FromCategory
        {
            get
            {
                return cat => new CategoryVM
                {
                    Id = cat.Id,
                    Name = cat.Name
                };
            }
        }

        public Category ToCategory()
        {
            return new Category 
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}