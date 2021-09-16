using EasySql.Databases;

namespace EasySql.SqlServer.Databases
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        public IDatabaseConnection Create(DbContextOptions options)
        {
            return new SqlServerDatabaseConnection(options.DatabaseProvider.ConnectionString);
        }
    }
}
