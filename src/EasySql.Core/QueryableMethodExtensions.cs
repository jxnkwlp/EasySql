using System;
using System.Linq;

namespace EasySql
{
    public static class QueryableMethodExtensions
    {
        public static string ToSqlText<T>(this IQueryable<T> quable)
        {
            throw new NotImplementedException();
        }
    }
}
