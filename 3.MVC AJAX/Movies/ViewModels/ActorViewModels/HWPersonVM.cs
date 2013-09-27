using Movies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Movies.ViewModels.ActorViewModels
{
    public class HWPersonVM
    {
        public int Id { get; set; }

        [Required,
        StringLength(100, ErrorMessage="Actors name is 100 symbols long max!")]
        public string Name { get; set; }
        
        [Range(typeof(DateTime), "1850-01-01", "2013-01-01", 
            ErrorMessage = "Birthdate is between 1850 and now")]
        public DateTime BirthDate { get; set; }

        public string Sex { get; set; }

        public static Expression<Func<Person, HWPersonVM>> FromEntity
        {
            get
            {
                return per => new HWPersonVM
                {
                    Id = per.Id,
                    Name = per.Name,
                    BirthDate = per.BirthDate ?? DateTime.Now                    
                };
            }
        }

    }
}