using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class OrderingExpression : SqlExpression
    {
        public OrderingExpression(SqlExpression expression, bool isDescending)
        {
            Expression = expression;
            IsDescending = isDescending;
        }

        public virtual SqlExpression Expression { get; }

        public virtual bool IsDescending { get; }

        public Expression Update(SqlExpression expression, bool isDescending)
        {
            return Expression == expression && IsDescending == isDescending ? this : new OrderingExpression(expression, isDescending);
        }
    }
}
