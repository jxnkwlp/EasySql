using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class SqlBinaryExpression : SqlExpression
    {
        private static readonly ISet<ExpressionType> _allowedOperators = new HashSet<ExpressionType>
        {
            ExpressionType.Add,
            ExpressionType.Subtract,
            ExpressionType.Multiply,
            ExpressionType.Divide,
            ExpressionType.Modulo,
            ExpressionType.And,
            ExpressionType.AndAlso,
            ExpressionType.Or,
            ExpressionType.OrElse,
            ExpressionType.LessThan,
            ExpressionType.LessThanOrEqual,
            ExpressionType.GreaterThan,
            ExpressionType.GreaterThanOrEqual,
            ExpressionType.Equal,
            ExpressionType.NotEqual,
        };

        internal static bool IsValidOperator(ExpressionType operatorType)
            => _allowedOperators.Contains(operatorType);

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
