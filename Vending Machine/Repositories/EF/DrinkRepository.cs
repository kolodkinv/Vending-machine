using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vending_Machine.Models.Product;

namespace Vending_Machine.Repositories.EF
{
    public class DrinkRepository: IRepository<Drink>
    {
        public IEnumerable<Drink> GetAll()
        {
            throw new NotImplementedException();
        }

        public Drink Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drink> Find(Func<Drink, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Drink item)
        {
            throw new NotImplementedException();
        }

        public void Update(Drink item)
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