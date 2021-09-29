using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.SqlServer.Databases;
using Microsoft.Extensions.DependencyInjection;

namespace EasySql.SqlServer
{
    public static class DbContextOptionBuilderExtensions
    {
        public static DbContextOptionBuilder UseSqlServer(this DbContextOptionBuilder builder, string connectionString)
        {
            builder.SetDatabaseProvider(new DatabaseProvider(connectionString));

            builder.AddService(ServiceDescriptor.Transient<IDatabaseConnectionFactory, DatabaseConnectionFactory>());
            builder.ReplaceService(ServiceDescriptor.Singleton<ITypeMappingConfiguration, SqlServerTypeMappingConfiguration>());
            builder.ReplaceService(ServiceDescriptor.Singleton<ISqlGenerationHelper, SqlServerSqlGenerationHelper>());

            return builder;
        }
    }
}
