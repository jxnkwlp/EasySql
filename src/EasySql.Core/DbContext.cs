using System;
using EasySql.DependencyInjection;
using EasySql.Query;
using EasySql.Query.SqlExpressions;

namespace EasySql
{
    public class DbContext : IDbContext
    {
        public DbContext(DbContextOptions options)
        {
            Options = options;
        }

        public DbContext(Action<DbContextOptionBuilder> builderAction = null)
        {
            var di = new InnerDI();

            var builder = new DbContextOptionBuilder(di);

            builder.UseServiceProvider(di);

            builderAction?.Invoke(builder);

            Options = builder.Options;
        }

        public DbContextOptions Options { get; }

        private IQueryExecutor CreateQueryExecutor()
        {
            return new QueryExecutor(Options);
        }

        public IEntityQueryable<T> Query<T>() where T : class
        {
            var queryExecutor = CreateQueryExecutor();

            var queryProvider = new EntityQueryProvider(queryExecutor);

            return new EntityQueryable<T>(queryProvider, new EntityQueryExpression(typeof(T)));
        }

        public void Add<T>(T instance) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Remove<T>(T instance) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Remove<T>(params object[] keys) where T : class
        {
            throw new System.NotImplementedException();
        }

        public void Update<T>(T instance) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}
