using System;

namespace EasySql.Query
{
    public static class QueryableMethodExtensions
    {
        public static string ToTraceSql<T>(this IEntityQueryable<T> source) where T : class
        {
            throw new NotImplementedException();
        }

        public static void Delete<T>(this IEntityQueryable<T> source) where T : class
        {
            throw new NotImplementedException();
        }

        public static void Update<T>(this IEntityQueryable<T> source, object updatedValue) where T : class
        {
            if (updatedValue == null)
                throw new ArgumentNullException(nameof(updatedValue));

            throw new NotImplementedException();
        }

    }
}
