using System;

namespace EasySql.Query.SqlExpressions
{
    public class ExistsExpression : SqlExpression
    {
        public ExistsExpression(SqlExpression expression, bool negated)
        {
            Expression = expression;
            Negated = negated;
        }

        public SqlExpression Expression { get; }

        public bool Negated { get; }

        public override Type Type => typeof(bool);
    }
}
