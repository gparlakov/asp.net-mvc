using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Teleritter.Models
{
    public class NewTelereetViewModel
    {
        [Required, StringLength(1000, ErrorMessage="Max length is 1000 symbols! Min is 2.", MinimumLength = 2)]
        public string Text { get; set; }
        
        public string Tags { get; set; }
          
        public string NewTag { get; set; }
    }
}