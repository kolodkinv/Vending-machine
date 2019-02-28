using Vending_Machine.Models.Products;

namespace Vending_Machine.Models
{
    public class ProductBasket
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}