using System;
using System.Data.Common;
using EasySql.Databases;
using Microsoft.Data.SqlClient;

namespace EasySql.SqlServer.Databases
{
    public class SqlServerDatabaseConnection : IDatabaseConnection
    {
        public SqlServerDatabaseConnection(string connectionString, int? commandTimeout = null)
        {
            ConnectionString = connectionString;
            CommandTimeout = commandTimeout;

            DbConnection = new SqlConnection(connectionString);
        }

        public Guid ConnectionId { get; set; }
        public DbConnection DbConnection { get; set; }
        public string ConnectionString { get; set; }
        public int? CommandTimeout { get; set; }

        public IDbContextTransaction Transaction { get; set; }

        public bool Close()
        {
            try
            {
                DbConnection.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        public bool Open()
        {
            try
            {
                DbConnection.Open();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }
    }
}
