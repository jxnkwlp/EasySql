using EasySql.Query;

namespace EasySql
{
    public interface IDbContext
    {
        IEntityQueryable<T> Query<T>() where T : class;

        void Add<T>(T instance) where T : class;

        void Update<T>(T instance) where T : class;

        void Remove<T>(T instance) where T : class;

        void Remove<T>(params object[] keys) where T : class;

    }
}
