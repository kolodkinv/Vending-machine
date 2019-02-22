using System;

namespace Vending_Machine.Exceptions
{
    public class BlockedMoneyException : Exception
    {
        public BlockedMoneyException(string message)
            : base(message)
        { }
    }
}