using System;

namespace Vending_Machine.Exceptions
{
    /// <summary>
    /// Исключение возникающее при переполнении стоимости товаров.
    /// </summary>
    public class BasketCostException: Exception
    {
        public BasketCostException(string message)
            : base(message)
        { }
    }
}