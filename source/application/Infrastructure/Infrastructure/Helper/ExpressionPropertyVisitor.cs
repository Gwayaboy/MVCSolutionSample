using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Panzea.DonorSpace.Infrastructure.Helper
{
    public class ExpressionPropertyVisitor : ExpressionVisitor
    {
        private readonly IList<PropertyInfo> _properties = new List<PropertyInfo>();
        public IEnumerable<PropertyInfo> GetPropertiesFrom(params Expression[] expressions)
        {
            Visit(new ReadOnlyCollection<Expression>(expressions));
            return _properties;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            var property = node.Member as PropertyInfo;
            if (property != null && !_properties.Contains(property))
            {
                _properties.Add(property);
            }

            return base.VisitMember(node);
        }



    }
}