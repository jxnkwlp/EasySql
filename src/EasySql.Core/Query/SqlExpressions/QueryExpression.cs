using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasySql.Infrastructure;

namespace EasySql.Query.SqlExpressions
{
    public class QueryExpression : SqlExpression
    {
        public Expression Predicate { get; private set; }
        public TableExpression Table { get; private set; }
        public List<OrderingExpression> Orderings { get; private set; }
        public bool IsDistinct { get; private set; }
        public SqlExpression Limit { get; private set; }
        public SqlExpression Offset { get; private set; }
        public List<SqlExpression> GroupBy { get; private set; }
        public List<ProjectionExpression> Projections { get; private set; }

        public string Alias { get; set; }

        public QueryResultType ResultType { get; private set; } = QueryResultType.Enumerable;

        public QueryExpression()
        {
            Orderings = new List<OrderingExpression>();
            Projections = new List<ProjectionExpression>();
            GroupBy = new List<SqlExpression>();
        }

        public void ApplyPredicate(Expression expression)
        {
            if (Predicate == null)
                Predicate = expression;
            else
            {
                Predicate = new SqlBinaryExpression(Predicate, expression, ExpressionType.AndAlso);
            }
        }

        public void SetIsDistinct(bool value)
        {
            IsDistinct = value;
        }

        public void SetTable(TableExpression table)
        {
            Table = table;
        }

        public void SetTable(EntityDefintion entity)
        {
            Table = new TableExpression(entity, entity.Name.Substring(0, 1).ToLower());

            foreach (var item in entity.Colunmns)
            {
                AddProjection(new ColumnExpression(item, null, Table.Alias));
            }
        }

        public void ClearProjections()
        {
            Projections.Clear();
        }

        public void AddProjection(SqlExpression expression)
        {
            Projections.Add(new ProjectionExpression(null, expression));
        }

        public void ApplyLimit(int count) => Limit = new SqlConstantExpression(count);

        public void ApplyOffset(int count) => Offset = new SqlConstantExpression(count);

        public void AddOrdering(OrderingExpression expression) => Orderings.Add(expression);

        public void ReverseOrdering()
        {
            Orderings = Orderings
                .Select(x => (OrderingExpression)x.Update(x.Expression, !x.IsDescending))
                .ToList();
        }

        public void AddGrouping(SqlExpression expression)
        {
            GroupBy.Add(expression);
        }

        public void ChangeResultType(QueryResultType resultType)
        {
            ResultType = resultType;
        }
    }
}
