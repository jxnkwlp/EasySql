using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.Infrastructure;
using Microsoft.Extensions.Logging;

namespace EasySql.Query
{
    public class QueryContextFactory : IQueryContextFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IDatabase _database;
        private readonly IEntityConfiguration _entityConfiguration;
        private readonly ITypeMappingConfiguration _typeMappingConfiguration;
        private readonly ISqlGenerationHelper _sqlGenerationHelper;
        private readonly ISqlCommandBuilder _sqlCommandBuilder;
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IQueryableMethodTranslator _queryableMethodTranslator;

        public QueryContextFactory(ILoggerFactory loggerFactory, IDatabase database, IEntityConfiguration entityConfiguration, ITypeMappingConfiguration typeMappingConfiguration, ISqlGenerationHelper sqlGenerationHelper, ISqlCommandBuilder sqlCommandBuilder, IDatabaseConnectionFactory databaseConnectionFactory, IQueryableMethodTranslator queryableMethodTranslator)
        {
            _loggerFactory = loggerFactory;
            _database = database;
            _entityConfiguration = entityConfiguration;
            _typeMappingConfiguration = typeMappingConfiguration;
            _sqlGenerationHelper = sqlGenerationHelper;
            _sqlCommandBuilder = sqlCommandBuilder;
            _databaseConnectionFactory = databaseConnectionFactory;
            _queryableMethodTranslator = queryableMethodTranslator;
        }

        public QueryContext Create(DbContextOptions options)
        {
            return new QueryContext(options, _loggerFactory, _database, _entityConfiguration, _typeMappingConfiguration, _sqlGenerationHelper, _sqlCommandBuilder, _databaseConnectionFactory, _queryableMethodTranslator);
        }
    }
}
