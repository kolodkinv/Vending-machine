using System;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Repositories.EF
{
    public class UnitOfWorkEF
    {
        private MachineContext _context;
        private EFRepository<Money> _moneyRepository;
        private EFRepository<Drink> _drinkRepository;
        private EFRepository<Basket> _basketRepository;
        private EFRepository<MoneyBasket> _moneyBasketRepository;
        private EFRepository<ProductBasket> _productBasketRepository;
        private EFRepository<Product> _productRepository;
        
        public UnitOfWorkEF(MachineContext context)
        {
            _context = context;
        }
        
        public EFRepository<MoneyBasket> MoneyBaskets
        {
            get { return _moneyBasketRepository ?? (_moneyBasketRepository = new EFRepository<MoneyBasket>(_context)); }
        }

        public EFRepository<ProductBasket> ProductBaskets
        {
            get { return _productBasketRepository ?? (_productBasketRepository = new EFRepository<ProductBasket>(_context)); }
        }
        
        public EFRepository<Product> Products
        {
            get { return _productRepository ?? (_productRepository = new EFRepository<Product>(_context)); }
        }
        
        public EFRepository<Money> Money
        {
            get { return _moneyRepository ?? (_moneyRepository = new EFRepository<Money>(_context)); }
        }

        public EFRepository<Drink> Drinks
        {
            get { return _drinkRepository ?? (_drinkRepository = new EFRepository<Drink>(_context)); }
        }
        
        public EFRepository<Basket> Baskets
        {
            get { return _basketRepository ?? (_basketRepository = new EFRepository<Basket>(_context)); }
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