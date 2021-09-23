using System;
using System.Linq;
using System.Linq.Expressions;
using EasySql.Infrastructure;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query
{
    public class QueryTranslator : ExpressionVisitor, IQueryTranslator
    {
        private readonly QueryExpression _queryExpression = new QueryExpression();

        private readonly QueryContext _queryContext;
        private readonly IEntityConfiguration _entityConfiguration;

        public QueryTranslator(QueryContext queryContext)
        {
            _queryContext = queryContext;
            _entityConfiguration = queryContext.Options.GetRequiredService<IEntityConfiguration>();
        }

        public SqlExpression Translate(Expression expression)
        {
            var result = Visit(expression);

            return _queryExpression;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            if (node is EntityQueryExpression entityQueryExpression)
            {
                var entityDefinition = entityQueryExpression.EntityDefintion;

                _queryExpression.SetTable(entityDefinition);

                return _queryExpression;
            }

            return base.VisitExtension(node);
        }

        private LambdaExpression GetLambdaExpression(Expression node)
        {
            if (node.NodeType == ExpressionType.Quote && node is UnaryExpression unaryExpression)
            {
                return unaryExpression.Operand as LambdaExpression;
            }

            return node as LambdaExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
            {
                var source = Visit(node.Arguments[0]) as QueryExpression;

                switch (node.Method.Name)
                {
                    case nameof(Queryable.All):
                        TranslateAll(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Any) when node.Arguments.Count == 1:
                        TranslateAny();
                        break;
                    case nameof(Queryable.Any) when node.Arguments.Count == 2:
                        TranslateAny(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.AsQueryable):
                        break;
                    case nameof(Queryable.Average):
                        TranslateAverage(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Contains): break;
                    case nameof(Queryable.Count) when node.Arguments.Count == 1:
                        TranslateCount();
                        break;
                    case nameof(Queryable.Count) when node.Arguments.Count == 2:
                        TranslateCount(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.LongCount) when node.Arguments.Count == 1:
                        TranslateCount();
                        break;
                    case nameof(Queryable.LongCount) when node.Arguments.Count == 2:
                        TranslateCount(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.DefaultIfEmpty):
                        TranslateDefaultIfEmpty();
                        break;
                    case nameof(Queryable.Distinct):
                        TranslateDistinct(source);
                        break;
                    case nameof(Queryable.ElementAt):
                        throw new NotSupportedException();
                    case nameof(Queryable.ElementAtOrDefault):
                        throw new NotSupportedException();
                    case nameof(Queryable.First) when node.Arguments.Count == 1:
                        TranslateFirst();
                        break;
                    case nameof(Queryable.First) when node.Arguments.Count == 2:
                        TranslateFirst(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.FirstOrDefault) when node.Arguments.Count == 1:
                        TranslateFirst();
                        break;
                    case nameof(Queryable.FirstOrDefault) when node.Arguments.Count == 2:
                        TranslateFirst(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.GroupBy):
                        TranslateGroupBy(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.GroupJoin): break;
                    case nameof(Queryable.Join): break;
                    case nameof(Queryable.Last) when node.Arguments.Count == 1:
                    case nameof(Queryable.LastOrDefault) when node.Arguments.Count == 1:
                        TranslateLast();
                        break;
                    case nameof(Queryable.Last) when node.Arguments.Count == 2:
                    case nameof(Queryable.LastOrDefault) when node.Arguments.Count == 2:
                        TranslateLast(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Max) when node.Arguments.Count == 1:
                        throw new NotSupportedException();
                    case nameof(Queryable.Max) when node.Arguments.Count == 2:
                        TranslateMax(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Min) when node.Arguments.Count == 1:
                        throw new NotSupportedException();
                    case nameof(Queryable.Min) when node.Arguments.Count == 2:
                        TranslateMin(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.OrderBy):
                        TranslateOrderBy(GetLambdaExpression(node.Arguments[1]), false);
                        break;
                    case nameof(Queryable.OrderByDescending):
                        TranslateOrderBy(GetLambdaExpression(node.Arguments[1]), true);
                        break;
                    case nameof(Queryable.Select):
                        TranslateSelect(source, GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.SelectMany):
                        throw new NotSupportedException();
                    case nameof(Queryable.Single) when node.Arguments.Count == 1:
                        TranslateSingle();
                        break;
                    case nameof(Queryable.Single) when node.Arguments.Count == 2:
                        TranslateSingle(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.SingleOrDefault) when node.Arguments.Count == 1:
                        TranslateSingle();
                        break;
                    case nameof(Queryable.SingleOrDefault) when node.Arguments.Count == 2:
                        TranslateSingle(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Skip):
                        TranslateSkip(node.Arguments[1] as ConstantExpression);
                        break;
#if NETSTANDARD2_1_OR_GREATER
                    case nameof(Queryable.SkipLast):
#endif
                    case nameof(Queryable.SkipWhile):
                        throw new NotSupportedException();
                    case nameof(Queryable.Sum):
                        TranslateSum(GetLambdaExpression(node.Arguments[1]));
                        break;
                    case nameof(Queryable.Take):
                        TranslateTake(node.Arguments[1] as ConstantExpression);
                        break;
#if NETSTANDARD2_1_OR_GREATER1
                    case nameof(Queryable.TakeLast):
#endif
                    case nameof(Queryable.TakeWhile):
                        throw new NotSupportedException();
                    case nameof(Queryable.ThenBy):
                        TranslateOrderBy(GetLambdaExpression(node.Arguments[1]), false);
                        break;
                    case nameof(Queryable.ThenByDescending):
                        TranslateOrderBy(GetLambdaExpression(node.Arguments[1]), true);
                        break;
                    case nameof(Queryable.Union): break;
                    case nameof(Queryable.Where):
                        return TranslateWhere(GetLambdaExpression(node.Arguments[1]));

                }

                return source;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Left is ConstantExpression constant1 && constant1.Value == null)
            {
                var o = Visit(node.Right);
                return new SqlUnaryExpression(o, node.NodeType, typeof(bool));
            }

            if (node.Right is ConstantExpression constant2 && constant2.Value == null)
            {
                var o = Visit(node.Left);
                return new SqlUnaryExpression(o, node.NodeType, typeof(bool));
            }

            var n1 = Visit(node.Left);

            var n2 = Visit(node.Right);

            return new SqlBinaryExpression(n1, n2, node.NodeType);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var nodeTypes = new[] {
                ExpressionType.Not,
                ExpressionType.NotEqual,
                ExpressionType.Equal,
                ExpressionType.Negate,
            };

            if (nodeTypes.Contains(node.NodeType))
            {
                var o = Visit(node.Operand);
                return new SqlUnaryExpression(o, node.NodeType, node.Type);
            }

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return new SqlConstantExpression(node.Value);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var entity = _entityConfiguration.FindEntity(node.Type);
            if (entity != null)
            {
                if (_queryExpression.Table.Entity.EntityType == entity.EntityType)
                {
                    return _queryExpression.Table;
                }

                return new TableExpression(entity, node.Name);
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var innerExpression = Visit(node.Expression);

            if (innerExpression is TableExpression table)
            {
                var column = table.Entity.FindColumn(node.Member);
                if (column == null)
                {
                    throw new Exception($"The column '{node.Member}' not found.");
                }

                return new ColumnExpression(column, null, table.Alias);
            }

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            foreach (var item in node.Members)
            {
                // _queryExpression.AddProjection(new ColumnExpression(null, item.Name));
            }

            return node;
        }

        protected virtual Expression TranslateWhere(LambdaExpression node)
        {
            // rewrite 
            if (node.Body is MemberExpression memberExpression)
            {
                var result = new SqlBinaryExpression(Visit(memberExpression), new SqlConstantExpression(true), ExpressionType.Equal);

                _queryExpression.ApplyPredicate(result);

                return node;
            }
            else
            {
                var result = Visit(node.Body);

                _queryExpression.ApplyPredicate(result);

                return node;
            }
        }

        protected virtual Expression TranslateAll(LambdaExpression node = null)
        {
            if (node != null)
            {
                TranslateWhere(node);
            }

            var exists = new ExistsExpression((SqlExpression)_queryExpression.Predicate, true);
            _queryExpression.ClearProjections();
            _queryExpression.AddProjection(exists);

            _queryExpression.ApplyLimit(1);

            return node;
        }

        protected virtual Expression TranslateAny(LambdaExpression node = null)
        {
            if (node != null)
            {
                TranslateWhere(node);
            }

            var exists = new ExistsExpression((SqlExpression)_queryExpression.Predicate, false);
            _queryExpression.ClearProjections();
            _queryExpression.AddProjection(exists);

            _queryExpression.ApplyLimit(1);

            return node;
        }

        protected virtual Expression TranslateAverage(LambdaExpression node)
        {
            return TranslateAggregate("AVG", node);
        }

        protected virtual Expression TranslateMax(LambdaExpression node)
        {
            return TranslateAggregate("MAX", node);
        }

        protected virtual Expression TranslateMin(LambdaExpression node)
        {
            return TranslateAggregate("MIN", node);
        }

        protected virtual Expression TranslateSum(LambdaExpression node)
        {
            return TranslateAggregate("SUM", node);
        }

        protected virtual Expression TranslateCount(LambdaExpression node = null)
        {
            return TranslateAggregate("COUNT", node);
        }

        protected virtual Expression TranslateAggregate(string name, LambdaExpression node = null)
        {
            if (node != null)
            {
                var column = Visit(node.Body) as ColumnExpression;

                _queryExpression.ClearProjections();
                _queryExpression.AddProjection(new ProjectionExpression(null, new SqlFunctionExpression(null, name, new SqlExpression[] { column })));
            }
            else
            {
                _queryExpression.AddProjection(new ProjectionExpression(null, new SqlFunctionExpression(null, name, new SqlExpression[] { new SqlFragmentExpression("*") })));
            }

            return node;
        }

        protected virtual Expression TranslateContains(Expression node)
        {
            var value = Visit(node);

            // TODO
            // _queryExpression.ApplyPredicate();

            return node;
        }

        protected virtual Expression TranslateDefaultIfEmpty()
        {
            throw new NotSupportedException();
        }

        protected virtual Expression TranslateDistinct(Expression source)
        {
            // TODO 
            // get all columns.

            _queryExpression.SetIsDistinct(true);

            return source;
        }

        protected virtual Expression TranslateFirst(LambdaExpression node = null)
        {
            if (node != null)
            {
                TranslateWhere(node);
            }

            _queryExpression.ApplyLimit(1);

            return node;
        }

        protected virtual Expression TranslateLast(LambdaExpression node = null)
        {
            if (node != null)
            {
                TranslateWhere(node);
            }

            if (_queryExpression.Orderings.Count == 0)
                throw new InvalidOperationException("Before 'Last' must have sort order.");

            _queryExpression.ReverseOrdering();
            _queryExpression.ApplyLimit(1);

            return node;
        }

        protected virtual Expression TranslateSingle(LambdaExpression node = null)
        {
            if (node != null)
            {
                TranslateWhere(node);
            }

            _queryExpression.ApplyLimit(1);

            return node;
        }

        protected virtual Expression TranslateSkip(ConstantExpression node)
        {
            _queryExpression.ApplyOffset(int.Parse(node.Value.ToString()));

            return node;
        }

        protected virtual Expression TranslateTake(ConstantExpression node)
        {
            _queryExpression.ApplyLimit(int.Parse(node.Value.ToString()));

            return node;
        }

        protected virtual Expression TranslateOrderBy(LambdaExpression node, bool isDescending = false)
        {
            var column = Visit(node) as SqlExpression;

            _queryExpression.AddOrdering(new OrderingExpression(column, isDescending));

            return node;
        }

        protected virtual Expression TranslateSelect(QueryExpression source, LambdaExpression node)
        {
            if (node.Parameters[0] == node.Body)
                return source;

            var result = Visit(node.Body);

            if (result is ColumnExpression column)
            {
                _queryExpression.AddProjection(column);
            }

            return node;
        }

        protected virtual Expression TranslateGroupBy(LambdaExpression node)
        {
            if (node.Body.NodeType == ExpressionType.New && node.Body is NewExpression newExpression)
            {
                foreach (var item in newExpression.Arguments)
                {
                    var c = Visit(item);
                    _queryExpression.AddGrouping(c as ColumnExpression);
                }
            }
            else
            {
                var selector = Visit(node.Body);
                if (selector is ColumnExpression columnExpression)
                {
                    _queryExpression.AddGrouping(columnExpression);
                }
            }

            return node;
        }

    }
}
