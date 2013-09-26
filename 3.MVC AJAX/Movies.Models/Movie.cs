using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, 
        StringLength(150, MinimumLength = 1, 
            ErrorMessage="Movie Title is 1 to 150 symbols long")]
        public string Title { get; set; }

        [Required, 
        RegularExpression(@"^(19[2-9][\d]|20[0-1][\d])$", 
            ErrorMessage="year is between 1920 and 2019")]
        public string Year { get; set; }

        public virtual Actor LeadingActor { get; set; }

        public virtual Actor LeadingActress { get; set; }

        public virtual Director Director { get; set; }

        public virtual Studio Studio { get; set; }
    }
}
