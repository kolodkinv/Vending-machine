using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private double _amount;
        private double _oddMoney;

        // Словарь продуктов. Ключ - продукт, значение - количество товара в корзине
        public ICollection<Product> Products { get; private set; }
        public ICollection<Money> Money { get; private set; }
        
        [Key]
        public string Session { get; private set; }
        public double Amount
        {
            get { return _amount; }
            private set
            {
                if (value >= 0)
                {
                    _amount = value;
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
        
        public void AddMoney(Money money)
        {
            if (money.Enable)
            {
                var amountDeposited = money.Cost * money.Count;
                Amount += amountDeposited;
                OddMoney += amountDeposited;
                /*var item = Money.FirstOrDefault(m => m.Id == money.Id);

                if (Moneys.ContainsKey(money))
                {
                    Moneys[money] += count;
                }
                else
                {
                    Moneys.Append(new KeyValuePair<Money, int>(money, 1));
                }*/
            }
            else
            {
                throw new BlockedMoneyException("Данный вид денег заблокирован");
            }
        }

        public void AddProducts(Product product, int count)
        {
            OddMoney -= product.Cost * count;
            
            /*
            if (Products.ContainsKey(product))
            {
                Products[product] += count;     
            }
            else
            {
                Products.Append(new KeyValuePair<Product, int>(product, 1));
            }*/
        }

        public double GetTotalCost()
        {
            return Products.Aggregate(0.0, (total, next) => next.Cost * next.Count + total);
        }
        
        public bool IsCorrectPayment()
        {
            if (Amount >= GetTotalCost() && Products.Count > 0)
            {
                // TODO Добавить проверку что деньги не заблокированы
                return true;
            }

            return false;
        }
    }
}