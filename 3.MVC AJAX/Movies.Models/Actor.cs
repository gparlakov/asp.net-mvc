using System.Collections.Generic;

namespace Movies.Models
{
    public class Actor : Person
    {
        private ICollection<Movie> movies;

        public Actor()
        {
            this.movies = new List<Movie>();
        }

        public virtual ICollection<Movie> Movies
        {
            get
            {
                return this.movies;
            }
            set
            {
                this.movies = value;
            }
        }
    }
}
