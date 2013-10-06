using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }
        public virtual AppUser Author { get; set; }

        [Required, StringLength(200, MinimumLength = 5, ErrorMessage = "Comment should be 5 to 100 symbols long!")]
        public string Text { get; set; }

        public int LaptopId { get; set; }
        public virtual Laptop Laptop { get; set; }
    }
}
