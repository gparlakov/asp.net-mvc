using System.Collections.Generic;
using System.Linq;

namespace TelerikedIn.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        //IQueryable<T> All(IEnumerable<string> entitiesToInclude);

        T GetById(int id);

        T GetById(string id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);
    }
}
