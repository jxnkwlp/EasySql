using System;
using EasySql.DependencyInjection;
using EasySql.Infrastructure;
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

        private IQueryExecutor CreateQueryExecutor(QueryContext queryContext)
        {
            return new QueryExecutor(queryContext);
        }

        public IEntityQueryable<T> Query<T>() where T : class
        {
            var entityConfiguration = Options.ServiceProvider.GetRequiredService<IEntityConfiguration>();

            var entityType = entityConfiguration.RegisterEntity(typeof(T));

            var queryContext = new QueryContext(Options);

            var queryExecutor = CreateQueryExecutor(queryContext);

            var queryProvider = new EntityQueryProvider(queryExecutor, queryContext);

            return new EntityQueryable<T>(queryProvider, new EntityQueryExpression(entityType));
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

        internal virtual T GetRequiredService<T>() where T : class
        {
            return Options.ServiceProvider.GetRequiredService<T>();
        }

    }
}
