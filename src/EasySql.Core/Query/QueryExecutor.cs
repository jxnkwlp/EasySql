using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using EasySql.Databases;
using EasySql.Infrastructure;
using EasySql.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("EasySql.Tests")]
namespace EasySql.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly DbContextOptions _options;

        //public static readonly ParameterExpression QueryContextParameter = Expression.Parameter(typeof(QueryExecutor), "QueryExecutor");

        public QueryExecutor(DbContextOptions options)
        {
            _options = options;

        }

        public TResult Execute<TResult>(Expression expression)
        {
            var scope = _options.ServiceProvider.CreateScope();

            var queryContextFactory = scope.ServiceProvider.GetRequiredService<IQueryContextFactory>();

            var context = queryContextFactory.Create(_options);

            var command = ToDatabaseCommand(context, expression);

            IDatabase database = context.Database;

            var connection = context.DatabaseConnectionFactory.Create(context.Options);

            return database.Execute<TResult>(command, new DatabaseCommandContext(connection));
        }

        internal IDatabaseCommand ToDatabaseCommand(QueryContext queryContext, Expression expression)
        {
            var sqlExpression = Translate(queryContext, expression) as QueryExpression;

            var commandBuilder = new SqlTranslator(queryContext).Translate(sqlExpression);

            var command = commandBuilder.Build();

            return command;
        }

        internal Expression Translate(QueryContext queryContext, Expression expression)
        {
            expression = new QueryExpressionRewriteVisitor().Visit(expression);

            return new QueryTranslator(queryContext).Translate(expression);
        }

    }
}
