using System;
using System.Linq;
using System.Linq.Expressions;
using EasySql.Infrastructure;

namespace EasySql.Query.SqlExpressions
{
    public class EntityQueryExpression : SqlExpression
    {
        public EntityQueryExpression(EntityDefintion entityDefintion)
        {
            Type = typeof(IQueryable<>).MakeGenericType(entityDefintion.EntityType);
            EntityType = entityDefintion.EntityType;
            EntityDefintion = entityDefintion;
        }

        public EntityDefintion EntityDefintion { get; }

        public Type EntityType { get; }

        public QueryExpression Query { get; set; }

        public override Type Type { get; }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            return this;
        }

    }
}
