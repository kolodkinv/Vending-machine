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
    /// <summary>
    /// Абстрактный торговый автомат. Позволяет работать с разными типами товаров, денег,
    /// а также этикеток товаров на витрине
    /// </summary>
    /// <typeparam name="TProduct">Тип продуктов, которые продает автомат</typeparam>
    /// <typeparam name="TMoney">Тип денег с которыми работает автомат</typeparam>
    /// <typeparam name="TImage">Тип картинок-этикетов, которые показывает автомат на витрине</typeparam>
    public abstract class VendingMachine<TProduct, TMoney, TImage>
        where TProduct : Product    
        where TMoney : Money       
        where TImage : Image        
    {
        private readonly UnitOfWorkEF _db;

        protected VendingMachine(UnitOfWorkEF db)
        {
            _db = db;
        }

        /// <summary>
        /// Покупка выбранный товаров за внесенные деньги и получение сдачи
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <returns>Список монет и их количество возвращаемое в качестве сдачи</returns>
        /// <exception cref="NotFoundException">Монета или продукт не найден</exception>
        /// <exception cref="OrderCostException">Сумма заказа превышает сумму монет</exception>
        public List<TMoney> Sell(Order order)
        {
            var oddMonies = new List<TMoney>();
            var oddMoney = 0;
            
            if (order.Products.Count > 0 && order.Money.Count > 0)
            {
                // Обрабатываем купюры в заказе и считаем общую сумму и сдачу
                foreach (var moneyInOrder in order.Money)
                {
                    var moneyInStore = _db.Money.Get(moneyInOrder.Id);          
                    if (moneyInStore != null && moneyInStore.Enable)
                    {
                        oddMoney += moneyInStore.Cost * moneyInOrder.Count;
                        moneyInStore.Count += moneyInOrder.Count;
                    }
                    else
                    {
                        throw new NotFoundException("Купюра не найдена");
                    }
                }
                    
                // Обрабатываем продукты в заказе и считаем размер сдачи
                foreach (var productInOrder in order.Products)
                {
                    var productInStore = _db.Products.Get(productInOrder.Id);
                    if (productInStore != null)
                    {
                        oddMoney -= productInStore.Cost * productInOrder.Count;
                        if (oddMoney < 0)
                        {
                            throw new OrderCostException("Стоимость товаров превышает внесенную сумму");
                        }
                        productInStore.Count -= productInOrder.Count;
                    }
                    else
                    {
                        throw new NotFoundException("Товар не найден");
                    }
                }
                _db.Save();

                // Получаем монеты для сдачи и забираем их из денежного хранилища
                oddMonies = GetOddMoney(oddMoney);
                foreach (var odd in oddMonies)
                {
                    var money = _db.Money.Get(odd.Id);
                    money.Count -= odd.Count;
                }
                
                _db.Save();
            }

            return oddMonies;
        }

        /// <summary>
        /// Получение сдачи из цифрового номинала в монетный
        /// </summary>
        /// <param name="oddMoney"></param>
        /// <returns></returns>
        private List<TMoney> GetOddMoney(int oddMoney)
        {
            var oddMonies = new List<TMoney>();
            
            if (oddMoney > 0)
            {
                var moneisInStore = _db.Money.GetAll()
                    .Where(m => m.Count > 0)
                    .OrderByDescending(m => m.Cost);
                
                foreach (var money in moneisInStore)
                {
                    var remainder = oddMoney % money.Cost;
                    money.Count = oddMoney / money.Cost;
                    
                    if (money.Count > 0)
                    {
                        oddMonies.Add(money as TMoney);
                        oddMoney -= money.Cost * money.Count;
                    }
                        
                    if (remainder == 0)
                    {
                        return oddMonies;
                    }
                }
            }

            return oddMonies;
        }
        
        public TImage GetImage(int id)
        {
            return (TImage) _db.Images.Get(id);
        }

        public void AddMoney(TMoney money)
        {
            _db.Money.Create(money);   
        }
        
        public void UpdateImage(TImage image)
        {
            _db.Images.Update(image);
        }
        
        public void RemoveImage(int id)
        {
            _db.Images.Delete(id);
        }
        
        public IEnumerable<Money> GetAllMoneis()
        {
            return _db.Money.GetAll();
        }
        
        public TMoney GetMoney(int id)
        {
            return (TMoney) _db.Money.Get(id);
        }

        public void UpdateMoney(TMoney money)
        {
            _db.Money.Update(money);
        }
        
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.GetWithInclude(p => p.Image);
        }

        public TProduct GetProduct(int id)
        {
            return (TProduct) _db.Products.GetWithInclude(p => p.Id == id, m => m.Image).FirstOrDefault();
        }

        public void UpdateProduct(TProduct product)
        {
            _db.Products.Update(product);
            _db.Save();
        }

        public void AddProduct(TProduct product)
        {
            _db.Products.Create(product);
        }
    }
}