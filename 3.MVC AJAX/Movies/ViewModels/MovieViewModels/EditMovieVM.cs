using System;
using System.Linq;

namespace Movies.ViewModels.MovieViewModels
{
    public class EditMovieVM : CreateMovieVM
    {
        public int? SelectedActorId { get; set; }

        public int? SelectedActressId { get; set; }

        public int? SelectedDirectorId { get; set; }
    }
}