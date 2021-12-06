using System.Collections.Generic;

namespace EasySql.Databases
{
    public interface IDatabaseCommandBuilder
    {
        IDatabaseCommand Build();

        IReadOnlyList<IDatabaseCommandParameter> Parameters { get; }

        IDatabaseCommandBuilder AddParameter(IDatabaseCommandParameter parameter);

        IDatabaseCommandBuilder Append(string value);

        IDatabaseCommandBuilder AppendLine(string value = null);

        IDatabaseCommandBuilder IncrementIndent();

        IDatabaseCommandBuilder DecrementIndent();

    }
}
