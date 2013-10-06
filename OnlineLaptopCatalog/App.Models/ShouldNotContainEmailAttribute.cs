using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class ShouldNotContainEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valueAsString = value as string;
            if (value == null)
            {
                return false;
            }

            if (!Regex.IsMatch(valueAsString, @"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,4}"))
            {
                return true;
            }

            return false;
        }
    }
}
