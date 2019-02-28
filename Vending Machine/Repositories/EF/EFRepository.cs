using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Vending_Machine.Repositories.EF
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        MachineContext _context;
        DbSet<TEntity> _dbSet;
 
        public EFRepository(MachineContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
         
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = Get(id);
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
 
        public IEnumerable<TEntity> GetWithInclude(Func<TEntity,bool> predicate, 
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query =  Include(includeProperties);
            return query.Where(predicate).ToList();
        }
 
        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private bool _disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    _context.Dispose();
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