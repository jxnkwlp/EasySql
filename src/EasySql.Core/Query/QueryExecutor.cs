using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using EasySql.Databases;
using EasySql.DependencyInjection;
using EasySql.Infrastructure;
using EasySql.Query.SqlExpressions;

[assembly: InternalsVisibleTo("EasySql.Tests")]
namespace EasySql.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        //public static readonly ParameterExpression QueryContextParameter = Expression.Parameter(typeof(QueryExecutor), "QueryExecutor");

        private readonly QueryContext _queryContext;
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IEntityConfiguration _entityConfiguration;

        public QueryExecutor(QueryContext queryContext)
        {
            _queryContext = queryContext;
            _databaseConnectionFactory = queryContext.Options.GetRequiredService<IDatabaseConnectionFactory>();
            _entityConfiguration = queryContext.Options.GetRequiredService<IEntityConfiguration>();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var command = ToDatabaseCommand(expression);

            IDatabase database = new Database();

            var connection = _databaseConnectionFactory.Create(_queryContext.Options);

            return database.Execute<TResult>(command, new DatabaseCommandContext(connection));
        }

        internal IDatabaseCommand ToDatabaseCommand(Expression expression)
        {
            var sqlExpression = Translate(expression) as SqlExpression;

            var commandBuilder = new SqlTranslator(_queryContext).Translate(sqlExpression);

            var command = commandBuilder.Build();

            return command;
        }

        internal Expression Translate(Expression expression)
        {
            expression = new QueryExpressionRewriteVisitor().Visit(expression);

            return new QueryTranslator(_queryContext).Translate(expression);
        }

    }
}
