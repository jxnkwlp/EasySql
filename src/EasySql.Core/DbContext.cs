using System;
using EasySql.Infrastructure;
using EasySql.Query;
using EasySql.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasySql
{
    public class DbContext : IDbContext
    {
        public DbContextOptions Options { get; }

        public ILoggerFactory LoggerFactory => Options.GetRequiredService<ILoggerFactory>();

        public DbContext(DbContextOptions options)
        {
            Options = options;
        }

        public DbContext(Action<DbContextOptionBuilder> builderAction = null)
        {
            var services = new ServiceCollection();

            var builder = new DbContextOptionBuilder(services);

            builderAction?.Invoke(builder);

            builder.UseInnerServiceProvider();

            Options = builder.Build();
        }

        //protected virtual void ConfigureServices(DbContextOptionBuilder optionBuilder)
        //{ 
        //}

        private IQueryExecutor CreateQueryExecutor()
        {
            return new QueryExecutor(Options);
        }

        public IEntityQueryable<T> Query<T>() where T : class
        {
            var entityConfiguration = Options.ServiceProvider.GetRequiredService<IEntityConfiguration>();

            var entityType = entityConfiguration.Register(typeof(T));

            var queryExecutor = CreateQueryExecutor();

            var queryProvider = new EntityQueryProvider(queryExecutor);

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
