using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vending_Machine.Models
{
    public class Money
    {
        private double _cost;
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
        [Column(TypeName = "varchar(128) character set utf8")]
        public string Name { get; set; }
        public double Cost
        {
            get => _cost;
            set
            {
                if (value > 0)
                {
                    _cost = value;
                }
                else
                {
                    throw new ArgumentException("Номинал должен быть больше 0");
                }
            }
        }
        public bool Enable { get; set; }
    }
}