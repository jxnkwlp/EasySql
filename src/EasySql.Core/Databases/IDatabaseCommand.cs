using System.Collections.Generic;

namespace EasySql.Databases
{
    public interface IDatabaseCommand
    {
        string CommandText { get; }

        IReadOnlyList<ISqlCommandParameter> Parameters { get; }

        int ExecuteNonQuery(DatabaseCommandContext context);

        object ExecuteScalar(DatabaseCommandContext context);

        ISqlDataReader ExecuteReader(DatabaseCommandContext context);
    }
}
