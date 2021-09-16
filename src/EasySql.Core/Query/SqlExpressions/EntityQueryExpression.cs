using System;
using System.Linq;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class EntityQueryExpression : SqlExpression
    {
        public EntityQueryExpression(Type entityType)
        {
            Type = typeof(IQueryable<>).MakeGenericType(entityType);
            EntityType = entityType;
        }

        public Type EntityType { get; }

        public QueryExpression Query { get; set; }

        public override Type Type { get; }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return this;
        }

    }
}
