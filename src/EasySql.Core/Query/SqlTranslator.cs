using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query
{
    public class SqlTranslator : ExpressionVisitor, ISqlTranslator
    {
        private static readonly Dictionary<ExpressionType, string> _operatorMap = new Dictionary<ExpressionType, string>
        {
            { ExpressionType.Equal, " = " },
            { ExpressionType.NotEqual, " <> " },
            { ExpressionType.GreaterThan, " > " },
            { ExpressionType.GreaterThanOrEqual, " >= " },
            { ExpressionType.LessThan, " < " },
            { ExpressionType.LessThanOrEqual, " <= " },
            { ExpressionType.AndAlso, " AND " },
            { ExpressionType.OrElse, " OR " },
            { ExpressionType.Add, " + " },
            { ExpressionType.Subtract, " - " },
            { ExpressionType.Multiply, " * " },
            { ExpressionType.Divide, " / " },
            { ExpressionType.Modulo, " % " },
            { ExpressionType.And, " & " },
            { ExpressionType.Or, " | " }
        };

        private readonly IDatabaseCommandBuilder _sqlCommandBuilder;

        protected virtual string AliasSeparator { get; } = " AS ";

        private readonly ISqlGenerationHelper _sqlGenerationHelper;
        private readonly ITypeMappingConfiguration _typeMappingConfiguration;

        public SqlTranslator(QueryContext queryContext)
        {
            _sqlCommandBuilder = queryContext.SqlCommandBuilder;
            _sqlGenerationHelper = queryContext.SqlGenerationHelper;
            _typeMappingConfiguration = queryContext.TypeMappingConfiguration;
        }

        public IDatabaseCommand CreateDatabaseCommand(SqlExpression expression)
        {
            if (expression is QueryExpression queryExpression)
            {
                var result = Visit(expression);

                return _sqlCommandBuilder.Build();
            }

            return null;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            switch (node)
            {
                case QueryExpression queryExpression: VisitQuery(queryExpression); break;
                case ColumnExpression columnExpression: VisitColumn(columnExpression); break;
                case OrderingExpression orderingExpression: VisitOrdering(orderingExpression); break;
                case SqlBinaryExpression sqlBinaryExpression: VisitSqlBinary(sqlBinaryExpression); break;
                case SqlConstantExpression sqlConstantExpression: VisitSqlConstant(sqlConstantExpression); break;
                case SqlUnaryExpression sqlUnaryExpression: VisitSqlUnary(sqlUnaryExpression); break;
                case TableExpression tableExpression: VisitTable(tableExpression); break;
                case ProjectionExpression projectionExpression: VisitProjection(projectionExpression); break;
                case ExistsExpression existsExpression: VisitExists(existsExpression); break;
                case SqlFunctionExpression sqlFunctionExpression: VisitSqlFunction(sqlFunctionExpression); break;
                case SqlParameterExpression sqlParameterExpression: VisitSqlParameter(sqlParameterExpression); break;
                case SqlFragmentExpression sqlFragmentExpression: VisitSqlFragment(sqlFragmentExpression); break;

            }

            return base.VisitExtension(node);
        }

        protected virtual Expression VisitQuery(QueryExpression query)
        {
            _sqlCommandBuilder.Append("SELECT ");

            GenerateTop(query);

            if (query.IsDistinct)
            {
                _sqlCommandBuilder.Append("DISTINCT ");
            }

            if (query.Projections.Any())
            {
                VisitList(query.Projections.ToArray(), () =>
                {
                    _sqlCommandBuilder.Append(", ");
                });
            }
            else
            {
                _sqlCommandBuilder.Append(" 1 ");
            }

            _sqlCommandBuilder.AppendLine().Append("FROM ");

            Visit(query.Table);

            if (query.Predicate != null)
            {
                _sqlCommandBuilder.AppendLine().Append("WHERE ");

                Visit(query.Predicate);
            }

            if (query.GroupBy.Any())
            {
                _sqlCommandBuilder.AppendLine().Append("GROUP BY ");

                VisitList(query.GroupBy.ToArray(), () =>
                {
                    _sqlCommandBuilder.Append(" , ");
                });
            }

            if (query.Orderings.Any())
            {
                _sqlCommandBuilder.AppendLine().Append("ORDER BY ");

                VisitList(query.Orderings.ToArray(), () =>
                {
                    _sqlCommandBuilder.Append(" , ");
                });
            }

            GenerateOffsetLimit(query);

            return query;
        }

        protected virtual void VisitList(Expression[] expressions, Action separator)
        {
            for (int i = 0; i < expressions.Length; i++)
            {
                Visit(expressions[i]);

                if (i < expressions.Length - 1)
                    separator();
            }
        }

        protected virtual void GenerateTop(QueryExpression expression)
        {
            if (expression.Limit != null)
            {
                _sqlCommandBuilder.Append("TOP ");
                Visit(expression.Limit);
            }
        }

        protected virtual Expression VisitColumn(ColumnExpression expression)
        {
            _sqlCommandBuilder
                .Append(_sqlGenerationHelper.DelimitIdentifier(expression.TableAlias))
                .Append(".")
                .Append(_sqlGenerationHelper.DelimitIdentifier(expression.Name));

            if (!string.IsNullOrEmpty(expression.Alias))
            {
                _sqlCommandBuilder
                    .Append(AliasSeparator)
                    .Append(_sqlGenerationHelper.DelimitIdentifier(expression.Alias));
            }

            _sqlCommandBuilder.Append(" ");

            return expression;
        }

        protected virtual Expression VisitOrdering(OrderingExpression expression)
        {
            Visit(expression.Expression);

            if (expression.IsDescending)
                _sqlCommandBuilder.Append(" DESC ");

            return expression;
        }

        protected virtual Expression VisitSqlBinary(SqlBinaryExpression expression)
        {
            Visit(expression.Left);

            _sqlCommandBuilder.Append(_operatorMap[expression.OperatorType]);

            Visit(expression.Right);

            return expression;
        }

        protected virtual Expression VisitSqlConstant(SqlConstantExpression expression)
        {
            if (expression.Type == null)
                _sqlCommandBuilder.Append("NULL ");
            else
            {
                var mappings = _typeMappingConfiguration.FindMapping(expression.Type);

                _sqlCommandBuilder.Append(mappings.GetConstantLiteral(expression.Value));

            }
            return expression;
        }

        protected virtual Expression VisitSqlUnary(SqlUnaryExpression expression)
        {
            switch (expression.OperatorType)
            {
                case ExpressionType.Not:
                    {
                        if (expression.Type == typeof(bool))
                        {
                            _sqlCommandBuilder.Append("NOT (");
                            Visit(expression.Operand);
                            _sqlCommandBuilder.Append(")");
                        }
                        else
                        {
                            _sqlCommandBuilder.Append("~");
                            Visit(expression.Operand);
                        }
                        break;
                    }
                case ExpressionType.NotEqual:
                    {
                        Visit(expression.Operand);
                        _sqlCommandBuilder.Append(" IS NOT NULL");
                    }
                    break;
                case ExpressionType.Negate:
                    {
                        _sqlCommandBuilder.Append("-");
                        Visit(expression.Operand);
                    }
                    break;
                case ExpressionType.Equal:
                    {
                        Visit(expression.Operand);
                        _sqlCommandBuilder.Append(" IS NULL");
                    }
                    break;
                case ExpressionType.Convert:
                    // TODO  
                    break;
            }

            return expression;
        }

        protected virtual Expression VisitTable(TableExpression expression)
        {
            _sqlCommandBuilder
                .Append(_sqlGenerationHelper.DelimitIdentifier(expression.Name, expression.Schame))
                .Append(AliasSeparator)
                .Append(_sqlGenerationHelper.DelimitIdentifier(expression.Alias));

            return expression;
        }

        protected virtual Expression VisitProjection(ProjectionExpression expression)
        {
            Visit(expression.Expression);

            return expression;
        }

        protected virtual void GenerateOffsetLimit(QueryExpression expression)
        {

        }

        protected virtual Expression VisitExists(ExistsExpression expression)
        {
            _sqlCommandBuilder.Append("EXISTS(");

            Visit(expression.Expression);

            _sqlCommandBuilder.Append(")");

            return expression;
        }

        protected virtual Expression VisitSqlFunction(SqlFunctionExpression expression)
        {
            if (expression.Operand != null)
            {
                Visit(expression.Operand);
            }

            if (!string.IsNullOrEmpty(expression.Schema))
                _sqlCommandBuilder.Append(expression.Schema).Append(".");

            _sqlCommandBuilder
                .Append(expression.Name)
                .Append(" (");

            VisitList(expression.Arguments.ToArray(), () =>
            {
                _sqlCommandBuilder.Append(", ");
            });

            _sqlCommandBuilder.Append(")");

            return expression;
        }

        protected virtual Expression VisitSqlParameter(SqlParameterExpression expression)
        {

            return expression;
        }

        protected virtual Expression VisitSqlFragment(SqlFragmentExpression expression)
        {
            _sqlCommandBuilder.Append(expression.Sql);

            return expression;
        }

    }
}
