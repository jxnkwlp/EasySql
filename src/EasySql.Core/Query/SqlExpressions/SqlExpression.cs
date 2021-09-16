using System;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public abstract class SqlExpression : Expression
    {
        public override ExpressionType NodeType => ExpressionType.Extension;

        public override Type Type => typeof(object);

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return this;
        }

    }
}
