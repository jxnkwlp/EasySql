using EasySql.Databases;

namespace EasySql.SqlServer.Databases
{
    public class SqlServerDatabaseProvider : IDatabaseProvider
    {
        public string Name => typeof(SqlServerDatabaseProvider).Assembly.FullName;

        public string Version { get; }

        public string ConnectionString { get; }

        public SqlServerDatabaseProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
