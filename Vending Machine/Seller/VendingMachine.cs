using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;

namespace Vending_Machine.Seller
{
    public class VendingMachine<TProduct, TMoney> : IMultySeller
        where TProduct : Product 
        where TMoney : Money
    {
        private readonly IRepository<TProduct> _productStorage;
        private readonly IRepository<TMoney> _moneyStorage;
        private readonly IRepository<Basket> _basketStorage;
        
        public VendingMachine(IRepository<TProduct> productStorage, IRepository<TMoney> moneyStorage, IRepository<Basket> basketStorage)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
            _basketStorage = basketStorage;        
        }

        public Basket CreateBasket()
        {
            var basket = new Basket();
            _basketStorage.Create(basket);
            return basket;
        }

        public Basket GetBasket(int id)
        {
            return _basketStorage.Get(id);
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

        public void UpdateMoney(TMoney money)
        {
            _moneyStorage.Update(money);
        }

        public void UpdateProduct(TProduct product)
        {
            _productStorage.Update(product);
        }
        
        public void AddMoneyToBasket(int idBasket, int idMoney, int count = 1)
        {
            var money = _moneyStorage.Get(idMoney);
            if (money != null)
            {
                if (money.Enable)
                {
                    money.Count = count;
                    var basket = _basketStorage.Get(idBasket);
                    if (basket != null)
                    {
                        basket.AddMoney(money);  
                    }
                    else
                    {
                        throw new NotFoundException("Корзина не найдена");
                    }
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

        public void AddProductToBasket(int idBasket, int idProduct, int count = 1)
        {
            var product = _productStorage.Get(idProduct);
            if (product != null)
            {
                product.Count = count;
                var basket = _basketStorage.Get(idBasket);
                if (basket != null)
                {
                    basket.AddProducts(product);  
                }
                else
                {
                    throw new NotFoundException("Корзина не найдена");
                } 
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
        public double Sell(int id)
        {
            var basket = _basketStorage.GetAll().First();
            var oddMoney = 0.0;
            if (basket != null && basket.IsCorrectPayment())
            {
                foreach (var product in basket.Products)
                {
                    var productInStore = _productStorage.Get(product.Id);
                    if (productInStore != null)
                    {
                        productInStore.Count -= product.Count;
                        _productStorage.Update(productInStore);
                    }          
                }

                foreach (var money in basket.Money)
                {
                    var moneyInStore = _moneyStorage.Get(money.Id);
                    if (moneyInStore != null)
                    {
                        moneyInStore.Count += moneyInStore.Count;
                        _moneyStorage.Update(moneyInStore);
                    }     
                }

                oddMoney = basket.OddMoney;
                _basketStorage.Delete(basket.Id);
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