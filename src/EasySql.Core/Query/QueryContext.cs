using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.Infrastructure;
using Microsoft.Extensions.Logging;

namespace EasySql.Query
{
    public class QueryContext
    {
        public QueryContext(DbContextOptions options, ILoggerFactory loggerFactory, IDatabase database, IEntityConfiguration entityConfiguration, ITypeMappingConfiguration typeMappingConfiguration, ISqlGenerationHelper sqlGenerationHelper, ISqlCommandBuilder sqlCommandBuilder, IDatabaseConnectionFactory databaseConnectionFactory, IQueryableMethodTranslator queryableMethodTranslator)
        {
            Options = options;
            LoggerFactory = loggerFactory;
            Database = database;
            EntityConfiguration = entityConfiguration;
            TypeMappingConfiguration = typeMappingConfiguration;
            SqlGenerationHelper = sqlGenerationHelper;
            SqlCommandBuilder = sqlCommandBuilder;
            DatabaseConnectionFactory = databaseConnectionFactory;
            QueryableMethodTranslator = queryableMethodTranslator;
        }

        public DbContextOptions Options { get; }
        public ILoggerFactory LoggerFactory { get; }
        public IDatabase Database { get; }
        public IEntityConfiguration EntityConfiguration { get; }
        public ITypeMappingConfiguration TypeMappingConfiguration { get; }
        public ISqlGenerationHelper SqlGenerationHelper { get; }
        public ISqlCommandBuilder SqlCommandBuilder { get; }
        public IDatabaseConnectionFactory DatabaseConnectionFactory { get; }
        public IQueryableMethodTranslator QueryableMethodTranslator { get; }
    }
}
