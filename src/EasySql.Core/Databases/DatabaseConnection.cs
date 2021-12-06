using System;
using System.Data.Common;

namespace EasySql.Databases
{
    public abstract class DatabaseConnection : IDatabaseConnection
    {
        public virtual Guid ConnectionId => Guid.NewGuid();

        public virtual string ConnectionString { get; protected set; }

        public virtual DbConnection DbConnection { get; protected set; }

        public virtual int? CommandTimeout { get; set; }

        public virtual IDbContextTransaction Transaction { get; set; }

        public virtual bool Close()
        {
            DbConnection.Close();
            return true;
        }

        public abstract DbConnection CreateDbConnection();

        public virtual void Dispose()
        {
            DbConnection?.Dispose();
        }

        public virtual bool Open()
        {
            DbConnection?.Open();
            return true;
        }

    }
}
