using System;
using EasySql.Databases;
using EasySql.Databases.TypeMappings;
using EasySql.Infrastructure;
using EasySql.Query;
using EasySql.Query.Translators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasySql
{
    public class DbContextOptionBuilder
    {
        private readonly DbContextOptions _options = null;

        public IServiceCollection Services { get; }

        public DbContextOptionBuilder(IServiceCollection services)
        {
            _options = new DbContextOptions(this);

            Services = services;

            RegisterCoreServices();
        }

        internal DbContextOptionBuilder RegisterCoreServices()
        {
            Services.AddLogging();

            Services.AddTransient<IQueryContextFactory, QueryContextFactory>();

            Services.AddSingleton<IEntityConfiguration, EntityConfiguration>();
            Services.AddSingleton<IEntityConfigurationLoader, EntityConfigurationLoader>();
            Services.AddSingleton<ITypeMappingConfiguration, TypeMappingConfiguration>();

            Services.AddSingleton<ISqlGenerationHelper, SqlGenerationHelper>();

            Services.AddTransient<IQueryableMethodTranslator, QueryableMethodTranslator>();

            Services.AddTransient<IDatabaseCommandBuilder, DatabaseCommandBuilder>();

            Services.AddTransient<IDatabase, Database>();

            Services.AddSingleton<IMethodCallExpressionTranslator, StringMethodCallExpressionTranslator>();
            Services.AddSingleton<IMethodCallExpressionTranslator, DateTimeMethodCallExpressionTranslator>();

            return this;
        }

        public DbContextOptionBuilder UseServiceProvider(IServiceProvider serviceProvider)
        {
            _options.SetServiceProvider(serviceProvider);

            return this;
        }

        internal DbContextOptionBuilder UseInnerServiceProvider()
        {
            _options.SetServiceProvider(Services.BuildServiceProvider(true));

            return this;
        }

        public DbContextOptionBuilder ReplaceService(ServiceDescriptor serviceDescriptor)
        {
            Services.Replace(serviceDescriptor);

            return this;
        }

        public DbContextOptionBuilder AddService(ServiceDescriptor serviceDescriptor)
        {
            Services.Add(serviceDescriptor);

            return this;
        }

        public DbContextOptionBuilder SetDatabaseProvider(IDatabaseProvider databaseProvider)
        {
            _options.SetDatabaseProvider(databaseProvider);

            return this;
        }

        //public DbContextOptionBuilder LogTo(Action<string> write)
        //{
        //    return this;
        //}

        public DbContextOptions Build()
        {
            return _options;
        }

        internal void ValidateOptions()
        {
        }
    }
}
