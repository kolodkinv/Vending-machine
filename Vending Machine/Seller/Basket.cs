using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Seller
{
    /// <summary>
    /// Корзина торгового автомата
    /// </summary>
    public class Basket
    {
        private double _amount;
        private double _oddMoney;

        [Key]
        public int Id { get; private set; }
        public IList<Product> Products { get; private set; }
        public IList<Money> Money { get; private set; }
        
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
                var moneyInBasket = Money.FirstOrDefault(p => p.Id == money.Id);
                if (moneyInBasket == null)
                {
                    money.Count = 1;
                    Money.Append(money);
                }
                else
                {
                    var index = Money.IndexOf(moneyInBasket);
                    Products[index].Count += 1;
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
            var productInBasket = Products.FirstOrDefault(p => p.Id == product.Id);
            if (productInBasket == null)
            {
                product.Count = count;
                Products.Append(product);
            }
            else
            {
                var index = Products.IndexOf(productInBasket);
                Products[index].Count += count;
            }         
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