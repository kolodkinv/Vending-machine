namespace Vending_Machine.Seller
{
    /// <summary>
    /// Интерфейс продавца, позволяющий продавать товары по череди.
    /// Т.е в один момент времени может быть только один покупатель как в реальном торговом автомате.
    /// </summary>
    public interface ISimpleSeller
    {
        /// <summary>
        /// Продать товар
        /// </summary>
        /// <returns>Сдача с покупки</returns>
        double Sell();
    }
}