using System;
using EasySql.Infrastructure;
using EasySql.Query;
using Microsoft.Extensions.Logging;

namespace EasySql.Databases
{
    public class DatabaseCommandContext
    {
        public DatabaseCommandContext(ILoggerFactory loggerFactory, IDatabaseConnection connection, IEntityConfiguration entityConfiguration, Type resultClrType, QueryType queryType)
        {
            LoggerFactory = loggerFactory;
            Connection = connection;
            EntityConfiguration = entityConfiguration;
            ResultClrType = resultClrType;
            QueryType = queryType;
        }

        public ILoggerFactory LoggerFactory { get; }

        public IDatabaseConnection Connection { get; }

        public IEntityConfiguration EntityConfiguration { get; }

        public Type ResultClrType { get; }

        public QueryType QueryType { get; }

    }
}
