using System;

namespace Vending_Machine.Models.Product
{
    public abstract class Product
    {
        private int _cost;    // стоимость в рублях
        private int _count;
        
        public string Name;
        
        public int Cost
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

        public int Count
        {
            get { return _count; }
            private set
            {
                if (value >= 0)
                {
                    _count = value;
                }
                else
                {
                    throw new ArgumentException("Количество товара должна быть больше 0");
                }
            }
        }
    }
}