using System;

namespace Vending_Machine.Exceptions
{
    public class NotFoundProductException: Exception
    {
        public NotFoundProductException(string message)
            : base(message)
        { }
    }
}