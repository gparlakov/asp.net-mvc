using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LibrarySystem.ViewModels.Books
{
    public class BookDetailsVM : BookVM
    {
        public string ISBN { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}