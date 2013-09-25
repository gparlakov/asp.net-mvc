using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movies.Models
{
    public class Studio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Studio Name can not be empty"), 
            RegularExpression(@"[\w\s-',]", 
            ErrorMessage = "Studio name should be between 5 and 150 symbols long containing alphanumeric and |-|,|'| symbols ")]
        public string Name { get; set; }
    }
}
