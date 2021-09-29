using System.Collections.Generic;
using EasySql.Query;

namespace EasySql.Databases
{
    public interface IDatabaseCommand
    {
        string CommandText { get; }

        QueryResultType ResultType { get; }

        IReadOnlyList<ISqlCommandParameter> Parameters { get; }

        int ExecuteNonQuery(DatabaseCommandContext context);

        object ExecuteScalar(DatabaseCommandContext context);

        ISqlDataReader ExecuteReader(DatabaseCommandContext context);
    }
}
