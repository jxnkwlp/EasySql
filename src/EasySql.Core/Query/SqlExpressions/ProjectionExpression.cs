using System;

namespace EasySql.Query.SqlExpressions
{
    public class ProjectionExpression : SqlExpression
    {
        public ProjectionExpression(string alias, SqlExpression expression)
        {
            Alias = alias;
            Expression = expression;
        }

        public virtual string Alias { get; set; }

        public SqlExpression Expression { get; }

        public override Type Type => Expression.Type;

    }
}
