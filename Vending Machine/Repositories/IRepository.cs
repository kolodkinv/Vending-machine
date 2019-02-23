using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vending_Machine.Repositories
{
    public interface IRepository<T> : IDisposable 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}