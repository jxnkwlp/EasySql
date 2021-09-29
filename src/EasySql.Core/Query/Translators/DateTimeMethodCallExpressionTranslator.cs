using System;
using System.Linq.Expressions;

namespace EasySql.Query.Translators
{
    public class DateTimeMethodCallExpressionTranslator : IMethodCallExpressionTranslator
    {
        public Expression Translate(ExpressionVisitor visitor, MethodCallExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
