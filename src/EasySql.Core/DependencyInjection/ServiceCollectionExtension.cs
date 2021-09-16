#if !NET461
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
            var builder = new DbContextOptionBuilder(new MicrosoftExtensionsDependencyInjectionServiceRegistor(services));

            builderAction?.Invoke(builder);

            services.AddTransient<DbContext>(p =>
            {
                builder.UseServiceProvider(p);

                return Activator.CreateInstance(typeof(T), builder.Options) as DbContext;
            });

            return services;
        }

    }

    public class MicrosoftExtensionsDependencyInjectionServiceRegistor : IServiceRegistor
    {
        private readonly IServiceCollection _services;

        public MicrosoftExtensionsDependencyInjectionServiceRegistor()
        {
        }

        public MicrosoftExtensionsDependencyInjectionServiceRegistor(IServiceCollection services)
        {
            _services = services;
        }

        public void AddService<T>(bool isSingleton = false) where T : class
        {
            if (isSingleton)
                _services.AddSingleton<T>();
            else
                _services.AddTransient<T>();
        }

        public void AddService<T>(Func<T> instanceFactory, bool isSingleton = false) where T : class
        {
            if (isSingleton)
                _services.AddSingleton<T>((_) => instanceFactory());
            else
                _services.AddTransient<T>((_) => instanceFactory());
        }

        public void AddService<T, TImpl>(bool isSingleton) where T : class where TImpl : class, T
        {
            if (isSingleton)
                _services.AddSingleton<T, TImpl>();
            else
                _services.AddTransient<T, TImpl>();
        }

    }
}
#endif
