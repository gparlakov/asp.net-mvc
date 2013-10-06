namespace App.Models.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using App.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(App.Models.DataContext context)
        {
            if (context.Roles.Count() == 0)
            {
                this.SeedRoles(context);
            }

            if (context.Laptops.Count() == 0)
            {
                this.SeedLaptops(context);
            }
        }
  
        private void SeedLaptops(DataContext context)
        {
            var rand = new Random();

            var user = new AppUser { UserName = "Test User", Email = "TestUser@text.com"  };

            var manufacturers = new List<Manufacturer> 
            {
                new Manufacturer{Name = "Toshiba"},
                new Manufacturer{Name = "Lenovo"},
                new Manufacturer{Name = "Hp"},
                new Manufacturer{Name = "Dell"},
                new Manufacturer{Name = "Sony"},
            };

            for (int i = 0; i < 20; i++)
            {
                var laptop = new Laptop 
                {
                    Model = "Laptop" + i,
                    Manufacturer = manufacturers[rand.Next(5)],
                    Monitor = (rand.NextDouble() * 6) + 10,
                    Price = (decimal)(rand.NextDouble() * 1000) + 1000,
                    Ram = rand.Next(2,32),
                    Votes = GetRandomVotes(rand, user),
                    Comments = GetRandomComments(rand, user),
                    Weight = (rand.NextDouble() * 2) + 1.5,
                    HardDisk = 500 + rand.Next(1500),
                    ImageUrl = "http://laptop.bg/system/images/17227/thumb/Inspiron_3521.jpg?1353944331"
                };

                context.Laptops.Add(laptop);
            }

            try
            {
                context.SaveChanges();

            }
            catch (Exception ex)
            {
            }
        }

        private ICollection<Comment> GetRandomComments(Random rand, AppUser user)
        {
            var count = rand.Next(5);

            var comments = new List<Comment>();

            for (int i = 0; i < count; i++)
            {
                comments.Add(new Comment { Author = user, Text = "Random comment by a random user" + count + i});
            }

            return comments;
        }

        private ICollection<Vote> GetRandomVotes(Random rand, AppUser user)
        {
            var count = rand.Next(10);

            var votes = new List<Vote>();

            for (int i = 0; i < count; i++)
            {
                votes.Add(new Vote { User = user });
            }

            return votes;
        }

        private void SeedRoles(App.Models.DataContext context)
        {
            context.Roles.Add(new Role { Name = "user" });
            context.Roles.Add(new Role { Name = "admin" });
            context.SaveChanges();
        }
    }
}
