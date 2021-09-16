using EasySql.Databases;

namespace EasySql.SqlServer.Databases
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public string Name => typeof(DatabaseProvider).Assembly.FullName;

        public string ConnectionString { get; set; }

        public DatabaseProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
