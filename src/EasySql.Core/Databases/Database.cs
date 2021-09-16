using System;
using System.Collections.Generic;

namespace EasySql.Databases
{
    public class Database : IDatabase
    {
        public TResult Execute<TResult>(IDatabaseCommand command, DatabaseCommandContext context)
        {
            if (typeof(TResult).IsAssignableFrom(typeof(IEnumerable<TResult>)))
            {
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
