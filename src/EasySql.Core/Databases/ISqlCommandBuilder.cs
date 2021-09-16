using System.Collections.Generic;

namespace EasySql.Databases
{
    public interface ISqlCommandBuilder
    {
        IDatabaseCommand Build();

        IReadOnlyList<ISqlCommandParameter> Parameters { get; }

        ISqlCommandBuilder AddParameter(ISqlCommandParameter parameter);

        ISqlCommandBuilder Append(string value);

        ISqlCommandBuilder AppendLine(string value = null);

        ISqlCommandBuilder IncrementIndent();

        ISqlCommandBuilder DecrementIndent();

    }
}
