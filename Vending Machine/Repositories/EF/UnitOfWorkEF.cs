using System;
using Microsoft.AspNetCore.Mvc.Razor;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Repositories.EF
{
    public class UnitOfWorkEF 
    {
        private MachineContext _context;
        private EFRepository<Money> _moneyRepository;
        private EFRepository<Drink> _drinkRepository;
        private EFRepository<Product> _productRepository;
        private EFRepository<Image> _imageRepository;
        
        public UnitOfWorkEF(MachineContext context)
        {
            _context = context;
        }
        
        public EFRepository<Product> Products
        {
            get { return _productRepository ?? (_productRepository = new EFRepository<Product>(_context)); }
        }
        
        public EFRepository<Image> Images
        {
            get { return _imageRepository ?? (_imageRepository = new EFRepository<Image>(_context)); }
        }
        
        public EFRepository<Money> Money
        {
            get { return _moneyRepository ?? (_moneyRepository = new EFRepository<Money>(_context)); }
        }

        public EFRepository<Drink> Drinks
        {
            get { return _drinkRepository ?? (_drinkRepository = new EFRepository<Drink>(_context)); }
        }
 
        public void Save()
        {
            _context.SaveChanges();
        }
 
        private bool disposed = false;
 
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}