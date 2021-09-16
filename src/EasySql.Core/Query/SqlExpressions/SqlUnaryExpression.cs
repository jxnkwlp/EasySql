using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class SqlUnaryExpression : SqlExpression
    {
        private static readonly ISet<ExpressionType> _allowedOperators = new HashSet<ExpressionType>
        {
            ExpressionType.Equal,
            ExpressionType.NotEqual,
            ExpressionType.Convert,
            ExpressionType.Not,
            ExpressionType.Negate
        };

        public SqlUnaryExpression(Expression operand, ExpressionType operatorType, Type type)
        {
            Operand = operand;
            OperatorType = operatorType;
            Type = type;
        }

        public Expression Operand { get; }

        public ExpressionType OperatorType { get; }

        public override Type Type { get; }

        public override ExpressionType NodeType => ExpressionType.Extension;

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return this;
        }

    }
}
