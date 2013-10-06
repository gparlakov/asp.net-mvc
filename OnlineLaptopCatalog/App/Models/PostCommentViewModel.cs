using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace App.Models
{
    public class PostCommentViewModel
    {
        public int LaptopId { get; set; }

        [ShouldNotContainEmail, Required]
        public string Comment { get; set; }
    }
}