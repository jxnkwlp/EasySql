using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using EasySql.Databases;
using EasySql.Infrastructure;
using EasySql.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("EasySql.Tests")]
namespace EasySql.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly DbContextOptions _options;

        public static readonly ParameterExpression QueryContextParameter = Expression.Parameter(typeof(QueryContext), "queryContext");

        public QueryExecutor(DbContextOptions options)
        {
            _options = options;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var scope = _options.ServiceProvider.CreateScope();

            var queryContextFactory = scope.ServiceProvider.GetRequiredService<IQueryContextFactory>();

            var context = queryContextFactory.Create(_options);

            expression = Translate(context, expression);

            var queryExpression = expression as QueryExpression;

            var command = GetCommand(context, queryExpression);

            IDatabase database = context.Database;

            var connection = context.DatabaseConnectionFactory.Create(context.Options);

            var commandContext = new DatabaseCommandContext(
                scope.ServiceProvider.GetRequiredService<ILoggerFactory>(),
                connection,
                scope.ServiceProvider.GetRequiredService<IEntityConfiguration>(),
                typeof(TResult),
                queryExpression.ResultType);

            return database.Execute<TResult>(command, commandContext);
        }

        internal IDatabaseCommand GetCommand(QueryContext queryContext, QueryExpression expression)
        {
            return new SqlTranslator(queryContext).CreateDatabaseCommand(expression);
        }

        internal QueryExpression Translate(QueryContext queryContext, Expression expression)
        {
            expression = new QueryExpressionRewriteVisitor().Visit(expression);

            return new QueryTranslator(queryContext).Translate(expression) as QueryExpression;
        }

    }
}
