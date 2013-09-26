using System.ComponentModel.DataAnnotations;
namespace Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required, RegularExpression(@"^(19[\d][\d]|20[\d]{2})$", ErrorMessage="year is between 1920 and 2013")]
        public string Year { get; set; }

        public virtual Actor LeadingActor { get; set; }

        public virtual Actor LeadingActress { get; set; }

        public virtual Director Director { get; set; }
    }
}
