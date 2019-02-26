using System.Collections.Generic;
using System.Drawing.Printing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;

namespace Vending_Machine.Seller
{
    public class VendingMachine<TProduct, TMoney> : ISeller
        where TProduct : Product 
        where TMoney : Money
    {
        private readonly IRepository<TProduct> _productStorage;
        private readonly IRepository<TMoney> _moneyStorage;
        private readonly IRepository<Basket> _basketStorage;
        private readonly Basket _basket;
        
        public VendingMachine(IRepository<TProduct> productStorage, IRepository<TMoney> moneyStorage, IRepository<Basket> basketStorage)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
            _basketStorage = basketStorage;
            _basket = new Basket();
        }

        public void CreateBasket(int id)
        {
            
        }

        public IEnumerable<TMoney> GetAllMoneis()
        {
            return _moneyStorage.GetAll();
        }

        public IEnumerable<TProduct> GetAllProducts()
        {
            return _productStorage.GetWithInclude(p => p.Image);
        }

        public TMoney GetMoney(int id)
        {
            return _moneyStorage.Get(id);
        }

        public TProduct GetProduct(int id)
        {
            return _productStorage.Get(id);
        }

        public void SetEnableMoney(int id, bool enable)
        {
            var money = _moneyStorage.Get(id);
            if (money != null && money.Enable != enable)
            {
                money.Enable = enable;
                _moneyStorage.Update(money);
            }
        }

        public void UpdateMoney(TMoney money)
        {
            _moneyStorage.Update(money);
        }

        public void UpdateProduct(TProduct product)
        {
            _productStorage.Update(product);
        }
        
        public void AddMoneyToBasket(int id, int count = 1)
        {
            var money = _moneyStorage.Get(id);
            if (money != null)
            {
                if (money.Enable)
                {
                    money.Count = count;
                    _basket.AddMoney(money);    
                }
                else
                {
                    throw new BlockedMoneyException("Данный вид денег заблокирован");
                }
            }
            else
            {
                throw new NotFoundException("Данный вид денег не найден");
            }
        }

        public void AddProductToBasket(int id, int count = 1)
        {
            var product = _productStorage.Get(id);
            if (product != null)
            {
                product.Count = count;
                _basket.AddProducts(product); 
            }
            else
            {
                throw new NotFoundException("Добавляемый товар не найден");
            }
        }
        
        /// <summary>
        /// Купить текущее содержимое корзины
        /// </summary>
        /// <returns></returns>
        public double Sell()
        {
            var oddMoney = 0.0;
            if (_basket.IsCorrectPayment())
            {
                foreach (var product in _basket.Products)
                {
                    //_productStorage.DecreaseItem(product.Id, product.Count);
                }

                foreach (var money in _basket.Money)
                {
                    //_moneyStorage.IncreaseItem(money.Id, money.Count);
                }

                oddMoney = _basket.OddMoney;
            }

            return oddMoney;
        }

        public void AddNewMoneyToStorage(TMoney money)
        {
            _moneyStorage.Create(money);   
        }

        public void AddNewProductToStorage(TProduct product)
        {
            _productStorage.Create(product);
        }
    }
}