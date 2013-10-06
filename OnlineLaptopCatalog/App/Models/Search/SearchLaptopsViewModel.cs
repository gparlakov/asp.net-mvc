using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Search
{
    public class SearchLaptopsViewModel
    {
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public double MaxPrice { get; set; }
    }
}