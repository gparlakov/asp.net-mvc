using Movies.Models;
using System;
using System.Linq;

namespace Movies.ViewModels
{
    public class CreateMovieVM : Movie
    {

        public IQueryable<System.Web.Mvc.SelectListItem> Actors { get; set; }
    }
}
