using System;
using System.Data.Common;

namespace EasySql.Databases
{
    public interface IDatabaseConnection
    {
        Guid ConnectionId { get; set; }

        string ConnectionString { get; set; }

        DbConnection DbConnection { get; set; }

        int? CommandTimeout { get; set; }

        IDbContextTransaction Transaction { get; set; }

        bool Open();

        bool Close();
    }
}
