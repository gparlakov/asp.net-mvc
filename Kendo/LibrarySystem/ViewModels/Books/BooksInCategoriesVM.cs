using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.ViewModels.Books
{
    public class BooksInCategoriesVM
    {
        public object Name { get; set; }

        public IEnumerable<BookVM> Books { get; set; }
    }
}