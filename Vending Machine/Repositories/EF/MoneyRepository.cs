using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vending_Machine.Models;

namespace Vending_Machine.Repositories.EF
{
    public class MoneyRepository: IRepository<Money>
    {
        private MachineContext _db;

        public MoneyRepository(MachineContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Money> GetAll()
        {
            return _db.Monies;
        }

        public Money Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Money> Find(Func<Money, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Money item)
        {
            _db.Monies.Add(item);
            _db.SaveChanges();
        }

        public void Update(Money item)
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
                    _db.Dispose();
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