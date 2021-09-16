using System;
using EasySql.DependencyInjection;

namespace EasySql
{
    public class DbContextOptionBuilder
    {
        private readonly DbContextOptions _options = new DbContextOptions();
        private readonly IServiceRegistor _serviceRegistor;

        public DbContextOptions Options => _options;

        public DbContextOptionBuilder(IServiceRegistor serviceRegistor)
        {
            _serviceRegistor = serviceRegistor;
        }

        internal DbContextOptionBuilder RegisterCoreServices()
        {
            return this;
        }

        public DbContextOptionBuilder UseServiceProvider(IServiceProvider serviceProvider)
        {
            Options.SetServiceProvider(serviceProvider);

            return this;
        }

        public DbContextOptionBuilder ReplaceService<T, TImpl>() where T : class where TImpl : class, T
        {
            _serviceRegistor.AddService<T, TImpl>();

            return this;
        }

        internal void ValidateOptions()
        {

        }
    }
}
