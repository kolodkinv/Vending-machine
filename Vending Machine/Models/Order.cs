using System;
using System.Collections.Generic;
using Vending_Machine.Exceptions;

namespace Vending_Machine.Models
{
    public class Order
    {
        private double _amount;
        private double _oddMoney;
 
        public int Id { get; set; }
        public IList<ProductInOrder> Products { get; set; }
        public IList<MoneyInOrder> Money { get; set; }

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
                    throw new OrderCostException("Стоимость заказа превышает сумму внесенных денег");
                }
            }
        }
    }
}