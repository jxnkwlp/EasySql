using System;
using Microsoft.Extensions.Logging;

namespace EasySql.Databases
{
    public class Database : IDatabase
    {
        private readonly ILogger<Database> _logger;

        public Database(ILogger<Database> logger)
        {
            _logger = logger;
        }

        public TResult Execute<TResult>(IDatabaseCommand command, DatabaseCommandContext context)
        {
            _logger.LogInformation($"Executed DbCommand: {command.CommandText}");

            var resultType = typeof(TResult);

            if (command.ResultType == Query.QueryResultType.Enumerable)
            {
                // TODO 
                var result = command.ExecuteReader(context);

                return default;
            }
            else
            {
                var result = command.ExecuteScalar(context);

                return (TResult)Convert.ChangeType(result, typeof(TResult));
            }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
