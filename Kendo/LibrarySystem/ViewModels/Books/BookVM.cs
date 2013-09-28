using System;
using System.Linq;

namespace LibrarySystem.ViewModels.Books
{
    public class BookVM
    {
        public object Title { get; set; }

        public object Author { get; set; }

        public int Id { get; set; }

        public string Category { get; set; }
    }
}