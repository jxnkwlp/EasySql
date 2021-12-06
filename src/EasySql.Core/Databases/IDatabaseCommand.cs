using System.Collections.Generic;
using System.Data.Common;

namespace EasySql.Databases
{
    public interface IDatabaseCommand
    {
        string CommandText { get; }

        IReadOnlyList<IDatabaseCommandParameter> Parameters { get; }

        DbCommand CreateDbCommand(DatabaseCommandContext context);

        int ExecuteNonQuery(DatabaseCommandContext context);

        object ExecuteScalar(DatabaseCommandContext context);

        IDatabaseDataReader ExecuteReader(DatabaseCommandContext context);

        // TODO 
        // ValueTask<int> ExecuteNonQueryAsync(DatabaseCommandContext context);

    }
}
