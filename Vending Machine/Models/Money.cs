using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Vending_Machine.Models
{
    public class Money
    {
        private int _cost;
        private int _count;
        
        [Key]
        public int Id { get; set; }

        public int Count
        {
            get => _count;
            set
            {
                if (value >= 0)
                {
                    _count = value;
                }
                else
                {
                    throw new ArgumentException("Количество денег не может быть меньше 0");
                }
            }
        }
        // TODO Убрать кодировку
        //[Column(TypeName = "varchar(128) character set utf8")]
        public string Name { get; set; }
        public int Cost
        {
            get => _cost;
            set
            {
                var availableCosts = new[] {1, 2, 5, 10};
                if (availableCosts.Contains(value))
                {
                    _cost = value;
                }
                else
                {
                    throw new ArgumentException("Данный номинал не принимается");
                }
            }
        }
        public bool Enable { get; set; }

        public ICollection<MoneyBasket> MoneyBaskets { get; set; }
    }
}