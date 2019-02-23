using System;

namespace Vending_Machine.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message)
            : base(message)
        { }
    }
}