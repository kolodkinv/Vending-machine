namespace Vending_Machine.Seller
{
    /// <summary>
    /// Интерфейс продавца, позволяющий продавать товары
    /// </summary>
    public interface ISeller
    {
        /// <summary>
        /// Продать товар
        /// </summary>
        /// <returns>Сдача с покупки</returns>
        double Sell();
    }
}