using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;
using Vending_Machine.Repositories;
using Vending_Machine.Repositories.EF;

namespace Vending_Machine.Seller
{
    public class VendingMachine<TProduct, TMoney> : IMultySeller
        where TProduct : Product 
        where TMoney : Money
    {
        private readonly IRepository<TProduct> _productStorage;
        private readonly IRepository<TMoney> _moneyStorage;
        private readonly IRepository<Basket> _basketStorage;
        private readonly IRepository<MoneyBasket> _moneyBasketStorage;
        private readonly IBasketHandler _basketHandler;
        private readonly UnitOfWorkEF _db;
        
        public VendingMachine(
            IRepository<TProduct> productStorage, 
            IRepository<TMoney> moneyStorage, 
            IRepository<Basket> basketStorage,
            IRepository<MoneyBasket> moneyBasketStorage,
            IBasketHandler basketHandler,
            UnitOfWorkEF db)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
            _basketStorage = basketStorage;
            _moneyBasketStorage = moneyBasketStorage;
            _basketHandler = basketHandler;
            _db = db;
        }

        public Basket CreateBasket()
        {
            var basket = new Basket();
            _db.Baskets.Create(basket);
            //_basketStorage.Create(basket);
            return basket;
        }

        public Basket GetBasket(int id)
        {
            return _db.Baskets.Get(id);
            //return _basketStorage.Get(id);
        }

        public IEnumerable<Money> GetAllMoneis()
        {
            return _db.Money.GetAll();
            //return _moneyStorage.GetAll();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.GetWithInclude(p => p.Image);
        }

        public Money GetMoney(int id)
        {
            return _db.Money.Get(id);
        }

        public Product GetProduct(int id)
        {
            return _db.Products.Get(id);
        }

        public void UpdateMoney(TMoney money)
        {
            _db.Money.Update(money);
        }

        public void UpdateProduct(TProduct product)
        {
            _db.Products.Update(product);
        }
        
        public void AddMoneyToBasket(int idBasket, int idMoney, int count = 1)
        {
            var money = _db.Money.Get(idMoney);
            if (money != null)
            {
                if (money.Enable)
                {
                    money.Count = count;
                    var basket = _db.Baskets.GetWithInclude(p => p.Id == idBasket, b => b.MoneyBaskets).FirstOrDefault();         
                    if (basket != null)
                    {
                        _basketHandler.AddMoney(basket, money);
                        //var moneyBasket = basket.MoneyBaskets;
                        //_db.MoneyBaskets.Create(moneyBasket.FirstOrDefault());
                        //_db.MoneyBaskets.Save();
                        _db.Save();
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
//                var basket = _basketStorage.GetWithInclude(p => p.Id == idBasket, b => b.Products).FirstOrDefault();   
//                if (basket != null)
//                {
//                    _basketHandler.AddProducts(basket, product); 
//                    _basketStorage.Save();
//                }
//                else
//                {
//                    throw new NotFoundException("Корзина не найдена");
//                } 
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
            return 1.0;
//            var basket = _basketStorage.GetWithInclude(p => p.Id == id, b => b.Money).FirstOrDefault();   
//            var oddMoney = 0.0;
//            if (basket != null && _basketHandler.IsCorrectPayment(basket))
//            {
//                foreach (var product in basket.Products)
//                {
//                    var productInStore = _productStorage.Get(product.Id);
//                    if (productInStore != null)
//                    {
//                        productInStore.Count -= product.Count;
//                        _productStorage.Update(productInStore);
//                    }          
//                }
//
//                foreach (var money in basket.Money)
//                {
//                    var moneyInStore = _moneyStorage.Get(money.Id);
//                    if (moneyInStore != null)
//                    {
//                        moneyInStore.Count += moneyInStore.Count;
//                        _moneyStorage.Update(moneyInStore);
//                    }     
//                }
//
//                oddMoney = basket.OddMoney;
//                _basketStorage.Delete(basket.Id);
//            }

//            return oddMoney;
        }

        public void AddNewMoneyToStorage(TMoney money)
        {
            _db.Money.Create(money);   
        }

        public void AddNewProductToStorage(TProduct product)
        {
            _db.Products.Create(product);
        }
    }
}