using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return _db.Monies.FirstOrDefault(m => m.Id == id);
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
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Money> GetWithInclude(params Expression<Func<Money, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Money> GetWithInclude(Func<Money, bool> predicate, params Expression<Func<Money, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Money> Include(params Expression<Func<Money, object>>[] includeProperties)
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