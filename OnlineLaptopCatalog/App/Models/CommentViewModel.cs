using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace App.Models
{
    public class CommentViewModel
    {
        public string Author { get; set; }

        public string Text { get; set; }

        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return com => new CommentViewModel 
                {
                    Author = com.Author.UserName,
                    Text = com.Text
                };
            }
        }
    }
}