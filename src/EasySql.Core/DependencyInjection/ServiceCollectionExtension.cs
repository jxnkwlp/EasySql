using System;
using Microsoft.Extensions.DependencyInjection;

namespace EasySql.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, Action<DbContextOptionBuilder> builderAction = null)
        {
            return AddDbContext<DbContext>(services, builderAction);
        }

        public static IServiceCollection AddDbContext<T>(this IServiceCollection services, Action<DbContextOptionBuilder> builderAction = null) where T : DbContext
        {
            var builder = new DbContextOptionBuilder(services);

            builderAction?.Invoke(builder);

            services.AddTransient<DbContext>(p =>
            {
                builder.UseServiceProvider(p);

                var dbcontext = Activator.CreateInstance(typeof(T), builder.Build()) as DbContext;

                return dbcontext;
            });

            return services;
        }

    }
}
