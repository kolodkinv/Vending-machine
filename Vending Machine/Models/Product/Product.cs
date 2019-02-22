using System;

namespace Vending_Machine.Models.Product
{
    public abstract class Product
    {
        private double _cost;    // стоимость в рублях
        
        public string Name;
        
        public double Cost
        {
            get { return _cost; }

            private set
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