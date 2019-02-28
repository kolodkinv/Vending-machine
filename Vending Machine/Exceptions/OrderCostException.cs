using System;

namespace Vending_Machine.Exceptions
{
    /// <summary>
    /// Исключение возникающее при переполнении стоимости товаров.
    /// </summary>
    public class OrderCostException: Exception
    {
        public OrderCostException(string message)
            : base(message)
        { }
    }
}