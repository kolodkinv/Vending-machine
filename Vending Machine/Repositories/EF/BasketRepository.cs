using System;
using System.Collections.Generic;
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