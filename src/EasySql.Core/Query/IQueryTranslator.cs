using System.Linq.Expressions;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query
{
    public interface IQueryTranslator
    {
        SqlExpression Translate(Expression expression);
    }
}
