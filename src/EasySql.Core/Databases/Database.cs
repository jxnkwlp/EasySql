using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace EasySql.Databases
{
    public class Database : IDatabase
    {
        private readonly MethodInfo _readerGetResultMethodInfo;

        private readonly ILogger<Database> _logger;

        public Database(ILogger<Database> logger)
        {
            _readerGetResultMethodInfo = typeof(IDatabaseDataReader).GetRuntimeMethods().Single(x => x.Name == "GetResult" && x.IsGenericMethod);

            _logger = logger;
        }

        public TResult Execute<TResult>(IDatabaseCommand command, DatabaseCommandContext context)
        {
            _logger.LogInformation($"Executed DbCommand: \r\n{command.CommandText}");

            var resultType = typeof(TResult);

            // _logger.LogInformation($"TResult type => {resultType}");

            if (context.QueryType == Query.QueryType.Enumerable)
            {
                var reader = command.ExecuteReader(context);

                var elementType = resultType.GetGenericArguments()[0];

                var temp = _readerGetResultMethodInfo.MakeGenericMethod(elementType).Invoke(reader, null);

                return (TResult)temp;
            }
            else
            {
                var result = command.ExecuteScalar(context);

                return (TResult)Convert.ChangeType(result, resultType);
            }
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
