using System;
using System.Linq;

namespace EasySql
{
    public static class QueryableMethodExtensions
    {
        /// <summary>
        ///  Dump current queryable expression to sql
        /// </summary> 
        /// <exception cref="InvalidOperationException"></exception>
        [Obsolete]
        public static string ToSqlText<T>(this IQueryable<T> quable)
        {
            //if (quable.Provider is EntityQueryProvider entityQueryProvider)
            //{
            //    var queryContext = entityQueryProvider.QueryContext;

            //    var command = new QueryExecutor(queryContext).ToDatabaseCommand(quable.Expression);

            //    return command.CommandText;
            //}

            throw new InvalidOperationException("The queryable can't use this method.");
        }
    }
}
