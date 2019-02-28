using System.ComponentModel.DataAnnotations;

namespace Vending_Machine.Models
{
    public class ProductInOrder
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }
    }
}