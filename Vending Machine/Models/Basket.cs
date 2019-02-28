using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vending_Machine.Exceptions;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Models
{
    public class Basket
    {
        private double _amount;
        private double _oddMoney;

        [Key] public int Id { get; set; }
        public ICollection<ProductBasket> ProductBaskets { get; set; }
        public ICollection<MoneyBasket> MoneyBaskets { get; set; }

        public double Amount
        {
            get { return _amount; }
            set
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
            set
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
    }
}