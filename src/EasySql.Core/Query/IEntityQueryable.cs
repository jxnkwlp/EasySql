using System.Linq;

namespace EasySql.Query
{
    public interface IEntityQueryable<T> : IQueryable<T>, IOrderedQueryable<T>
    {

    }
}
