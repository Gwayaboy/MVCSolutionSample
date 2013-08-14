using System;

namespace Intrigma.DonorSpace.Core.ApplicationMessage
{
    public class BusinessRuleException : ApplicationException
    {
        public string[] PropertyNames { get; protected set; }

        public BusinessRuleException(string message, string propertyName = null) : base(message)
        {
            PropertyNames = new[] {propertyName};
        }

        public BusinessRuleException(string message, string[] propertyNames)
            : base(message)
        {
            PropertyNames = propertyNames;
        }
    }
}