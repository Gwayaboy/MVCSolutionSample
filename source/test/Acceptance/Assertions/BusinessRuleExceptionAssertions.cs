using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Specialized;
using Intrigma.DonorSpace.Core.ApplicationMessage;
using Intrigma.DonorSpace.Infrastructure.Helper;

namespace Intrigma.DonorSpace.Acceptance.Assertions
{
    public static class BusinessRuleExceptionAssertions
    {
       
        public static ExceptionAssertions<BusinessRuleException> WithPropertiesOf<T>(this ExceptionAssertions<BusinessRuleException> businessRuleExceptionAssertions, params Expression<Func<T, object>>[] propertySelectors)
            where T:class 
        {

            IEnumerable<string> expectedPropertyNames;

            if (propertySelectors != null)
            {

                var expressions = propertySelectors.Cast<Expression>().ToArray();
                
                expectedPropertyNames =
                    new ExpressionPropertyVisitor()
                        .GetPropertiesFrom(expressions)
                        .Select(p => p.Name);
            }
            else
            {
                expectedPropertyNames = typeof(T).GetProperties().Select(x => x.Name);
            }

            var businessRuleException = (BusinessRuleException)businessRuleExceptionAssertions.Subject;
            
            businessRuleException.PropertyNames.Should().ContainInOrder(expectedPropertyNames);

            return businessRuleExceptionAssertions;
        }
    }
}