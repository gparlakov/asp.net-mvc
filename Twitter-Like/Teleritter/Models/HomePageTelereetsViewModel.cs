using System;
using System.Collections.Generic;
using System.Linq;

namespace Twitter.Models
{
    public class HomePageTelereetsViewModel
    {
        public int UsersCount { get; set; }

        public int TelereetsCount { get; set; }

        public IEnumerable<TelereetViewModel> Telereets { get; set; }
    }
}