using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace EasySql.Databases
{
    public class DatabaseCommand : IDatabaseCommand
    {
        public DatabaseCommand(string commandText, IReadOnlyList<ISqlCommandParameter> parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }

        public string CommandText { get; }

        public IReadOnlyList<ISqlCommandParameter> Parameters { get; }

        public int ExecuteNonQuery(DatabaseCommandContext context)
        {
            var command = CreateDbCommand(context);

            return command.ExecuteNonQuery();
        }

        public ValueTask<int> ExecuteNonQueryAsync(DatabaseCommandContext context)
        {
            throw new NotImplementedException();
        }

        public ISqlDataReader ExecuteReader(DatabaseCommandContext context)
        {
            var command = CreateDbCommand(context);

            var reader = command.ExecuteReader();

            return new SqlDataReader(reader);
        }

        public object ExecuteScalar(DatabaseCommandContext context)
        {
            var command = CreateDbCommand(context);

            return command.ExecuteScalar();
        }

        public ValueTask<object> ExecuteScalarAsync(DatabaseCommandContext context)
        {
            throw new NotImplementedException();
        }

        private DbCommand CreateDbCommand(DatabaseCommandContext context)
        {
            context.Connection.Open();

            var command = context.Connection.DbConnection.CreateCommand();

            command.CommandText = CommandText;

            // TODO 

            return command;
        }
    }
}
