using System;
using System.ComponentModel.DataAnnotations;
using Vending_Machine.Storage;

namespace Vending_Machine.Models.Product
{
    public class Product : ICount
    {
        private double _cost;    // стоимость в рублях

        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public double Cost
        {
            get { return _cost; }

            set
            {
                if (value >= 0)
                {
                    _cost = value;
                }
                else
                {
                    throw new ArgumentException("Стоимость товара должна быть больше 0");
                }
            }
        }        
    }
}