using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.SqlServer.Databases;

namespace EasySql.SqlServer
{
    public static class DbContextOptionBuilderExtensions
    {
        public static DbContextOptionBuilder UseSqlServer(this DbContextOptionBuilder builder, string connectionString)
        {
            builder.Options.SetDatabaseProvider(new DatabaseProvider(connectionString));

            builder.ReplaceService<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            builder.ReplaceService<ITypeMappingConfiguration, SqlServerTypeMappingConfiguration>();
            builder.ReplaceService<ISqlGenerationHelper, SqlServerSqlGenerationHelper>();

            return builder;
        }
    }
}
