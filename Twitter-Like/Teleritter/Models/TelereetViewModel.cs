using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Teleritter.Models;

namespace Teleritter.Models
{
    public class TelereetViewModel
    {
        public string Text { get; set; }

        public string Author { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }

        public static Expression<Func<Telereet, TelereetViewModel>> FromTelereet
        {
            get
            {
                return t => new TelereetViewModel                 
                {
                    Tags = t.Tags.AsQueryable().Select(TagViewModel.FromTag),
                    Text = t.Text,
                    Author = t.Author.UserName,
                    PostedOn = t.PostedOn
                };
            }
        }

        public DateTime PostedOn { get; set; }
    }
}