using System.Linq.Expressions;

namespace EasySql.Query.Translators
{
    public interface IMethodCallExpressionTranslator
    {
        Expression Translate(ExpressionVisitor visitor, MethodCallExpression expression);
    }
}
