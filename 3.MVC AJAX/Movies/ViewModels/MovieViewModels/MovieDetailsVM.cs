using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.ViewModels.MovieViewModels
{
    public class MovieDetailsVM
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string LeadingActor { get; set; }

        public string LeadingActress { get; set; }

        public string Director { get; set; }

        public  string Studio{ get; set; }
    }
}