using System.Collections.Generic;
using System.Drawing.Printing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Storage;

namespace Vending_Machine.Seller
{
    public class VendingMachine: ISeller
    {
        private readonly Storage<Product> _productStorage;
        private readonly Storage<Money> _moneyStorage;
        private readonly Basket _basket;
        
        public VendingMachine(Storage<Product> productStorage, Storage<Money> moneyStorage)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
            _basket = new Basket();
        }

        public IEnumerable<Money> GetAllMoneis()
        {
            return _moneyStorage.GetAll();
        }

        public Money GetMoney(int id)
        {
            return _moneyStorage.GetItem(id);
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

        public void AddNewMoneyToStorage(Money money)
        {
            _moneyStorage.PutItem(money);   
        }

        public void IncreaseMoneyInStorage(int id, int count)
        {
            _moneyStorage.IncreaseItem(id, count);
        }

        public void AddNewProductToStorage(Product product)
        {
            _productStorage.PutItem(product);
        }

        public void IncreaseProductInStorage(int id, int count)
        {
            _productStorage.IncreaseItem(id, count);
        }
    }
}