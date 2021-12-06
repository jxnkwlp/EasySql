using EasySql.Databases;

namespace EasySql.SqlServer.Databases
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        public IDatabaseConnection Create(DbContextOptions options)
        {
            var provider = options.DatabaseProvider as SqlServerDatabaseProvider;

            return new SqlServerDatabaseConnection(provider.ConnectionString);
        }
    }
}
