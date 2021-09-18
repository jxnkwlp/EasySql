using System.Linq.Expressions;
using EasySql.Databases;
using EasySql.DependencyInjection;
using EasySql.Infrastructure;

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
            var sqlExpression = new QueryTranslator(_queryContext).Translate(expression);

            var commandBuilder = new SqlTranslator(_queryContext).Translate(sqlExpression);

            var command = commandBuilder.Build();

            IDatabase database = new Database();

            var connection = _databaseConnectionFactory.Create(_queryContext.Options);

            return database.Execute<TResult>(command, new DatabaseCommandContext(connection));
        }

    }
}
