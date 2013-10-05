using System;
using System.Linq.Expressions;
using Teleritter.Models;

namespace Teleritter.Models
{
    public class TagViewModel
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public static Expression<Func<Tag, TagViewModel>> FromTag
        {
            get
            {
                return t => new TagViewModel
                {
                    TagId = t.Id,
                    Name = t.Name,
                };
            }
        }
    }
}