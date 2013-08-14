using System;

namespace Panzea.DonorSpace.Infrastructure.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string message)
            : base(message)
        { }
    }
}