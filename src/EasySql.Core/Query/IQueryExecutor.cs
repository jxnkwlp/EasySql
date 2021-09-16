using System.Linq.Expressions;

namespace EasySql.Query
{
    public interface IQueryExecutor
    {
        TResult Execute<TResult>(Expression expression);
    }
}
