using System;

namespace Vending_Machine.Exceptions
{
    public class CountException : Exception
    {
        public CountException(string message)
            : base(message)
        { }
    }
}