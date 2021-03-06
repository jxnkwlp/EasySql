using System.Linq.Expressions;

namespace EasySql.Query
{
    public interface IQueryableMethodTranslator
    {
        Expression Translate(ExpressionVisitor visitor, MethodCallExpression expression);
    }
}
