using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Dmicic.DataAnnotations.PartialValidation
{
    internal static class Helper
    {
        public static string GetPropertyName(Expression propertyExpression)
        {
            var member = propertyExpression as MemberExpression;
            if (member == null)
            {
                var unary = propertyExpression as UnaryExpression;
                if (unary != null && unary.NodeType == ExpressionType.Convert)
                    member = unary.Operand as MemberExpression;
            }

            if (member != null && member.Member.MemberType == MemberTypes.Property)
                return member.Member.Name;

            throw new ArgumentException("Property not found.", "propertyExpression");
        }
    }
}
