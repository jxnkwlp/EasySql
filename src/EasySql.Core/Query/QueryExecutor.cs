using System.Linq.Expressions;
using EasySql.Databases;
using EasySql.DependencyInjection;
using EasySql.Infrastructure;

namespace EasySql.Query
{
    public class QueryExecutor : IQueryExecutor
    {
        //public static readonly ParameterExpression QueryContextParameter = Expression.Parameter(typeof(QueryExecutor), "QueryExecutor");

        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly DbContextOptions _options;
        private readonly IEntityConfiguration _entityConfiguration;

        public QueryExecutor(DbContextOptions options, IEntityConfiguration entityConfiguration)
        {
            _options = options;
            _databaseConnectionFactory = options.ServiceProvider.GetRequiredService<IDatabaseConnectionFactory>();
            _entityConfiguration = entityConfiguration;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            var sqlExpression = new QueryTranslator(_entityConfiguration).Translate(expression);

            var commandBuilder = new SqlTranslator().Translate(sqlExpression);

            var command = commandBuilder.Build();

            IDatabase database = new Database();

            var connection = _databaseConnectionFactory.Create(_options);

            return database.Execute<TResult>(command, new DatabaseCommandContext(connection));
        }

    }
}
