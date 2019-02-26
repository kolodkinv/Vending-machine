using System.ComponentModel.DataAnnotations;

namespace Vending_Machine.Dto
{
    public class MoneyInBasket
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int IdBasket { get; set; }
    }
}