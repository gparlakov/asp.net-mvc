using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TelerikedIn.Data;
using Teleritter.Data.Interfaces;
using Teleritter.Models;

namespace Teleritter.Data
{
    public class UowData : IUowData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new ApplicationDbContext())
        {
        }

        public UowData(DbContext context)
        {
            this.context = context;
        }
        public IRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
        }

        public IRepository<Telereet> Telreets
        {
            get { return this.GetRepository<Telereet>(); }
        }
                     
        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }


        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }    
}
