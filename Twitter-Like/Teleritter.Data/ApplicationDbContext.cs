using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using Teleritter.Models;

namespace Teleritter.Data
{
    public class ApplicationDbContext : IdentityDbContextWithCustomUser<ApplicationUser>
    {
        public IDbSet<Telereet> Tweets { get; set; }

        public IDbSet<Tag> Tags { get; set; }
    }
}
