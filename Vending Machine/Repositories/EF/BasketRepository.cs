using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vending_Machine.Seller;

namespace Vending_Machine.Repositories.EF
{
    public class BasketRepository : IRepository<Basket>
    {
        public IEnumerable<Basket> GetAll()
        {
            throw new NotImplementedException();
        }

        public Basket Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Basket> Find(Func<Basket, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Basket item)
        {
            throw new NotImplementedException();
        }

        public void Update(Basket item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Basket> GetWithInclude(params Expression<Func<Basket, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Basket> GetWithInclude(Func<Basket, bool> predicate, params Expression<Func<Basket, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Basket> Include(params Expression<Func<Basket, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        private bool _disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    //db.Dispose();
                }
            }
            _disposed = true;
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}