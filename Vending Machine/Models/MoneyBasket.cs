namespace Vending_Machine.Models
{
    public class MoneyBasket
    {
        public int MoneyId { get; set; }
        public Money Money { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}