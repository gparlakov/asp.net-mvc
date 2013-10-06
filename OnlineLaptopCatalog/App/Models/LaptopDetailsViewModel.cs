using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace App.Models
{
    public class LaptopDetailsViewModel
    {
        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public double Monitor { get; set; }

        public int Ram { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string AdditionalParts { get; set; }

        public double? Weight { get; set; }

        public string Description { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public static Expression<Func<Laptop, LaptopDetailsViewModel>> FromLaptop
        {
            get
            {
                return laptop => new LaptopDetailsViewModel
                {
                    Id = laptop.Id,
                    Model = laptop.Model,
                    Monitor = laptop.Monitor,
                    AdditionalParts = laptop.AdditionaParts,
                    Comments = laptop.Comments.AsQueryable().Select(CommentViewModel.FromComment),
                    Description = laptop.Description,
                    ImageUrl = laptop.ImageUrl,
                    Manufacturer = laptop.Manufacturer.Name,
                    Ram = laptop.Ram,
                    Weight = laptop.Weight,
                    Price = laptop.Price
                };
            }
        }

        public int Id { get; set; }
    }
}