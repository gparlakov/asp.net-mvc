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
            this.Tags = new List<TagViewModel>();
        }

        public int Id { get; set; }

        [UIHint("String")]
        [Required, StringLength(100, ErrorMessage = "Text must be 2 to 100 symbols long!", MinimumLength = 2)]
        public string Text { get; set; }

        [UIHint("User")]
        public UserAdminViewModel Author { get; set; }

        [UIHint("Tags")]
        public IEnumerable<TagViewModel> Tags { get; set; }

        public static Expression<Func<Telereet, TelereetAdminViewModel>> FromTelereet
        {
            get
            {
                return t => new TelereetAdminViewModel
                {
                    Id = t.Id,
                    Tags = t.Tags.AsQueryable().Select(TagViewModel.FromTag),
                    Text = t.Text,
                    Author = new UserAdminViewModel
                    {
                        Id = t.Author.Id,
                        Username = t.Author.UserName
                    },
                    PostedOn = t.PostedOn
                };
            }
        }

        [UIHint("DateTime")]
        public DateTime PostedOn { get; set; }
    }
}