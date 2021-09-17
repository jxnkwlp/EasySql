using System;
using EasySql.DependencyInjection;
using EasySql.Infrastructure;

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

            RegisterCoreServices();
        }

        internal DbContextOptionBuilder RegisterCoreServices()
        {
            _serviceRegistor.AddService<IEntityConfiguration, EntityConfiguration>();
            _serviceRegistor.AddService<IEntityConfigurationLoader, EntityConfigurationLoader>();

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
