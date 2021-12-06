using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Extensions.Logging;

namespace EasySql.Databases
{
    public class DatabaseCommand : IDatabaseCommand
    {
        public DatabaseCommand(string commandText, IReadOnlyList<IDatabaseCommandParameter> parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }

        public string CommandText { get; }

        public IReadOnlyList<IDatabaseCommandParameter> Parameters { get; }

        public int ExecuteNonQuery(DatabaseCommandContext context)
        {
            var logger = context.LoggerFactory.CreateLogger<DatabaseCommand>();

            using (var command = CreateDbCommand(context))
            {
                try
                {
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Executed dbCommand failed.");

                    throw;
                }
            }
        }

        public IDatabaseDataReader ExecuteReader(DatabaseCommandContext context)
        {
            var logger = context.LoggerFactory.CreateLogger<DatabaseCommand>();

            using (var command = CreateDbCommand(context))
            {
                try
                {
                    var reader = command.ExecuteReader();

                    return new DatabaseDataReader(reader, context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Executed dbCommand failed.");

                    throw;
                }
            }
        }

        public object ExecuteScalar(DatabaseCommandContext context)
        {
            var logger = context.LoggerFactory.CreateLogger<DatabaseCommand>();

            using (var command = CreateDbCommand(context))
            {
                try
                {
                    return command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Executed dbCommand failed.");

                    throw;
                }
            }
        }

        public DbCommand CreateDbCommand(DatabaseCommandContext context)
        {
            var connection = context.Connection.CreateDbConnection();

            context.Connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = CommandText;

            // TODO 

            return command;
        }

    }
}
