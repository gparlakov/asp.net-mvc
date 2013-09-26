using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.ViewModels
{
    public class CreateMovieVM : Movie
    {
        public int? LeadingActorId { get; set; }

        public int? LeadingActressId { get; set; }

        public int? DirectorId { get; set; }

        public IEnumerable<PersonVM> Actors { get; set; }

        public IEnumerable<PersonVM> Directors { get; set; }

        public IEnumerable<PersonVM> Actresses { get; set; }
    }
}
