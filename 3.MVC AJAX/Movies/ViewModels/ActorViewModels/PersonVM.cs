using Movies.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Movies.ViewModels
{
    public class PersonVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Expression<Func<Person, PersonVM>> FromEntity
        {
            get
            {
                return act => new PersonVM
                {
                    Id = act.Id,
                    Name = act.Name
                };
            }
        }
    }
}