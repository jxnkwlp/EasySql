using System;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class SqlBinaryExpression : SqlExpression
    {
        public SqlBinaryExpression(Expression left, Expression right, ExpressionType operatorType)
        {
            Left = left;
            Right = right;
            OperatorType = operatorType;
        }

        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public ExpressionType OperatorType { get; }

        public override ExpressionType NodeType => ExpressionType.Extension;

        public override Type Type => typeof(bool);

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return this;
        }
    }
}
