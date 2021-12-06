using System;
using System.Data.Common;

namespace EasySql.Databases
{
    public interface IDatabaseConnection : IDisposable
    {
        Guid ConnectionId { get; }

        string ConnectionString { get; }

        DbConnection DbConnection { get; }

        int? CommandTimeout { get; set; }

        IDbContextTransaction Transaction { get; set; }

        bool Open();

        bool Close();

        DbConnection CreateDbConnection();
    }
}
