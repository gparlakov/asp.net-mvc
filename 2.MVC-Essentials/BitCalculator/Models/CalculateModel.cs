using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTest.Models
{
    public class CalculateModel
    {
        public double NormalizedValue { get; set; }

        public Units Unit { get; set; }

        public int Kilo { get; set; }
    }
}