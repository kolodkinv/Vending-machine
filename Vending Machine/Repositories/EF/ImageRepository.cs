using System;
using System.Collections.Generic;
using System.Linq;
using Vending_Machine.Models;

namespace Vending_Machine.Repositories.EF
{
    public class ImageRepository : IRepository<Image>
    {
        private MachineContext _db;

        public ImageRepository(MachineContext db)
        {
            _db = db;
        }

        public IEnumerable<Image> GetAll()
        {
            return _db.Images;
        }

        public Image Get(int id)
        {
            return _db.Images.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Image> Find(Func<Image, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(Image item)
        {
            _db.Images.Add(item);
            _db.SaveChanges();
        }

        public void Update(Image item)
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