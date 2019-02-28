using System.ComponentModel.DataAnnotations;

namespace Vending_Machine.Models
{
    public class MoneyInOrder
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
    }
}