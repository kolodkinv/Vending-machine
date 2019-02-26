using System.Collections.Generic;
using System.Drawing.Printing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Storage;

namespace Vending_Machine.Seller
{
    public class VendingMachine<TProduct, TMoney> : ISeller
        where TProduct : Product 
        where TMoney : Money
    {
        private readonly Storage<TProduct> _productStorage;
        private readonly Storage<TMoney> _moneyStorage;
        private readonly Basket _basket;
        
        public VendingMachine(Storage<TProduct> productStorage, Storage<TMoney> moneyStorage)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
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
            return _productStorage.GetAllWithInclude(p => p.Image);
        }

        public TMoney GetMoney(int id)
        {
            return _moneyStorage.GetItem(id);
        }

        public TProduct GetProduct(int id)
        {
            return _productStorage.GetItem(id);
        }

        public void SetEnableMoney(int id, bool enable)
        {
            var money = _moneyStorage.GetItem(id);
            if (money != null && money.Enable != enable)
            {
                money.Enable = enable;
                _moneyStorage.UpdateItem(money);
            }
        }

        public void UpdateMoney(TMoney money)
        {
            _moneyStorage.UpdateItem(money);
        }

        public void UpdateProduct(TProduct product)
        {
            _productStorage.UpdateItem(product);
        }
        
        public void AddMoneyToBasket(int id, int count = 1)
        {
            var money = _moneyStorage.GetItem(id);
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
            var product = _productStorage.GetItem(id);
            if (product != null)
            {
                _basket.AddProducts(product, count); 
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
                    _productStorage.DecreaseItem(product.Id, product.Count);
                }

                foreach (var money in _basket.Money)
                {
                    _moneyStorage.IncreaseItem(money.Id, money.Count);
                }

                oddMoney = _basket.OddMoney;
            }

            return oddMoney;
        }

        public void AddNewMoneyToStorage(TMoney money)
        {
            _moneyStorage.PutItem(money);   
        }

        public void AddNewProductToStorage(TProduct product)
        {
            _productStorage.PutItem(product);
        }
    }
}