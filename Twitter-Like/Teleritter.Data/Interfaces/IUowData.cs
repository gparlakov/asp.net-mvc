using System;
using System.Linq;
using TelerikedIn.Data;
using Teleritter.Models;
using Teleritter.Models;

namespace Teleritter.Data.Interfaces
{
    public interface IUowData : IDisposable
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Telereet> Telreets { get; }
        
        int SaveChanges();
    }
}
