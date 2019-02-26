using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Repositories.EF
{
    public class DrinkRepository: IRepository<Drink>
    {
        private MachineContext _db;

        public DrinkRepository(MachineContext db)
        {
            _db = db;
        }
        
        public IEnumerable<Drink> GetAll()
        {
            return _db.Drinks;
        }

        public Drink Get(int id)
        {
            return _db.Drinks.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Drink> Find(Func<Drink, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Drink item)
        {
            _db.Drinks.Add(item);
            _db.SaveChanges();
        }

        public void Update(Drink item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drink> GetWithInclude(params Expression<Func<Drink, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drink> GetWithInclude(Func<Drink, bool> predicate, params Expression<Func<Drink, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Drink> Include(params Expression<Func<Drink, object>>[] includeProperties)
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