using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vending_Machine.Models.Products
{
    public class Product
    {
        private int _cost;    // стоимость в рублях

        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public int Cost
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