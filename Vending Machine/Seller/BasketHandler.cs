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
    public class BasketHandler : IBasketHandler
    {
        public void AddMoney(Basket basket, Money money)
        {
            if (money.Enable)
            {
                var amountDeposited = money.Cost * money.Count;
                basket.Amount += amountDeposited;
                basket.OddMoney += amountDeposited;
                var moneyInBasket = basket.MoneyBaskets.FirstOrDefault(p => p.MoneyId == money.Id);
                if (moneyInBasket == null)
                {
                    money.Count = 1;
                    moneyInBasket = new MoneyBasket
                    {
                        Basket = basket,
                        BasketId = basket.Id,
                        Money = money,
                        MoneyId = money.Id
                    };
                    basket.MoneyBaskets.Add(moneyInBasket);
                    
                    //basket.MoneyBaskets.Add(money);
                }
                else
                {
//                    var currentMoney = basket.Money.FirstOrDefault(p => p.Id == moneyInBasket.Id);
//                    if (currentMoney != null)
//                    {
//                        currentMoney.Count += 1;
//                    }
                }
            }
            else
            {
                throw new BlockedMoneyException("Данный вид денег заблокирован");
            }
        }

        public void AddProducts(Basket basket, Product product)
        {
            basket.OddMoney -= product.Cost * product.Count;
            /*var productInBasket = basket.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productInBasket == null)
            {
                basket.Products.Add(product);
            }
            else
            {
                var currentProduct = basket.Products.FirstOrDefault(p => p.Id == productInBasket.Id);
                if (currentProduct != null)
                {
                    currentProduct.Count += 1;
                }
            }  */       
        }

        public double GetTotalCost(Basket basket)
        {
            return 1.0;
            //return basket.Products.Aggregate(0.0, (total, next) => next.Cost * next.Count + total);
        }

        public bool IsCorrectPayment(Basket basket)
        {
            /*if (basket.Amount >= GetTotalCost(basket) && basket.Products.Count > 0)
            {
                // TODO Добавить проверку что деньги не заблокированы
                return true;
            }*/

            return false;
        }
    }
}