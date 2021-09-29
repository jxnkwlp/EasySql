using System.Collections.Generic;
using EasySql.Query;

namespace EasySql.Databases
{
    public interface ISqlCommandBuilder
    {
        QueryResultType QueryResultType { get; set; }

        IDatabaseCommand Build();

        IReadOnlyList<ISqlCommandParameter> Parameters { get; }

        ISqlCommandBuilder AddParameter(ISqlCommandParameter parameter);

        ISqlCommandBuilder Append(string value);

        ISqlCommandBuilder AppendLine(string value = null);

        ISqlCommandBuilder IncrementIndent();

        ISqlCommandBuilder DecrementIndent();

    }
}
