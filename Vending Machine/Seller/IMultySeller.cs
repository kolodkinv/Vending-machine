namespace Vending_Machine.Seller
{
    /// <summary>
    /// Интерфейс продавца, позволяющий продавать товары паралелльно.
    /// Т.е в один момент времени может обслуживать несколько покупатель
    /// </summary>
    public interface IMultySeller
    {
        /// <summary>
        /// Совершить сдеку
        /// </summary>
        /// <param name="id">Ид сделки</param>
        /// <returns></returns>
        double Sell(int id);
    }
}