﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Movies.Models
{
    // You can add profile data for the user by adding more properties to your User class,
    // please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : User
    {

    }

    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Sex> Sexes { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}