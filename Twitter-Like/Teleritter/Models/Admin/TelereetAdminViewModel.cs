using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Teleritter.Models
{
    public class TelereetAdminViewModel
    {
        public TelereetAdminViewModel()
        {
            this.Tags = new List<string>();
        }

        public int Id { get; set; }

        [UIHint("String"), DataType(DataType.MultilineText)]
        [Required, StringLength(100, ErrorMessage = "Text must be 2 to 100 symbols long!", MinimumLength = 2)]
        public string Text { get; set; }

        [UIHint("User"), Required]
        public string Author { get; set; }

        [UIHint("Tags")]
        public IEnumerable<string> Tags { get; set; }

        [UIHint("DateTime")]
        public DateTime PostedOn { get; set; }

        public static Expression<Func<Telereet, TelereetAdminViewModel>> FromTelereet
        {
            get
            {
                return t => new TelereetAdminViewModel
                {
                    Id = t.Id,
                    Tags = t.Tags.AsQueryable().Select(tag => tag.Name),
                    Text = t.Text,
                    Author =  t.Author.UserName,
                    PostedOn = t.PostedOn
                };
            }
        }
    }
}