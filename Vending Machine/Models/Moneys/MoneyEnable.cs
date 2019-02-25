using System.ComponentModel.DataAnnotations;

namespace Vending_Machine.Models.Moneys
{
    public class MoneyEnable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Enable { get; set; }
    }
}