using System.Data.Common;
using EasySql.Databases;
using Microsoft.Data.SqlClient;

namespace EasySql.SqlServer.Databases
{
    public class SqlServerDatabaseConnection : DatabaseConnection
    {
        public SqlServerDatabaseConnection(string connectionString, int? commandTimeout = null)
        {
            ConnectionString = connectionString;
            CommandTimeout = commandTimeout;
        }

        public override DbConnection CreateDbConnection()
        {
            DbConnection = new SqlConnection(ConnectionString);

            return DbConnection;
        }

    }
}
