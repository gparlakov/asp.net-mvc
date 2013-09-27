using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required, RegularExpression(@"[\w\s'-]{5,100}", ErrorMessage = "Name is 5 to 100 symbols long!")]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
