using System;
using System.Linq;
using App.Models;

namespace App.Data.Interfaces
{
    public interface IUowData : IDisposable
    {
        IRepository<AppUser> Users { get; }

        IRepository<Vote> Votes { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Laptop> Laptops { get; }

        IRepository<Manufacturer> Manufacturers { get; }

        int SaveChanges();
    }
}
