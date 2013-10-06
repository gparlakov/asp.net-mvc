using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace App.Models
{
    public class DataContext : IdentityDbContextWithCustomUser<AppUser>
    {
        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Laptop> Laptops { get; set; }

        public IDbSet<Manufacturer> Manufacturers { get; set; }

    }
}
