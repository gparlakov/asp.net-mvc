using Movies.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movies.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Year { get; set; }

        public static Expression<Func<Movie, MovieVM>> FromEntity
        {
            get
            {
                return mov => new MovieVM
                {
                    Id = mov.Id,
                    Title = mov.Title,
                    Year = mov.Year
                };
            }
        }
    }
}