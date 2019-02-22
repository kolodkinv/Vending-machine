using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;

namespace Vending_Machine.Seller
{
    /// <summary>
    /// Корзина торгового автомата
    /// </summary>
    public class Basket
    {
        private double _money;
        private double _oddMoney;

        // Словарь продуктов. Ключ - продукт, значение - количество товара в корзине
        public Dictionary<Product, int> Products { get; private set; }
        public Dictionary<Money, int> Moneys { get; private set; }
        public double Money
        {
            get { return _money; }
            private set
            {
                if (value >= 0)
                {
                    _money = value;
                }
                else
                {
                    throw new ArgumentException("Сумма внесенных денег не может быть отрицательной");
                }
            }
        }
        public double OddMoney
        {
            get { return _oddMoney; }
            private set
            {
                if (value >= 0)
                {
                    _oddMoney = value;
                }
                else
                {
                    throw new BasketCostException("Стоимость заказа превышает сумму внесенных денег");    
                }
            }
        }
      
        public string UserId { get; private set; }
        
        public Basket()
        {
            Products = new Dictionary<Product, int>();
            Money = 0;
            OddMoney = 0;
        }
        
        public void AddMoney(Money money, int count)
        {
            if (money.Enable)
            {
                var amountDeposited = money.Cost * count;
                Money += amountDeposited;
                OddMoney += amountDeposited;
                if (Moneys.ContainsKey(money))
                {
                    Moneys[money] += count;
                }
                else
                {
                    Moneys.Append(new KeyValuePair<Money, int>(money, 1));
                }
            }
            else
            {
                throw new BlockedMoneyException("Данный вид денег заблокирован");
            }
        }

        public void AddProducts(Product product, int count)
        {
            OddMoney -= product.Cost * count;
            if (Products.ContainsKey(product))
            {
                Products[product] += count;     
            }
            else
            {
                Products.Append(new KeyValuePair<Product, int>(product, 1));
            }
        }

        public double GetTotalCost()
        {
            return Products.Aggregate(0.0, (total, next) => next.Key.Cost * next.Value + total);
        }
        
        public bool IsCorrectPayment()
        {
            if (Money >= GetTotalCost())
            {
                // TODO Добавить проверку что деньги не заблокированы
                return true;
            }

            return false;
        }
        
    }
}