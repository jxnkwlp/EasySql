using System;
using EasySql.Databases;
using EasySql.Infrastructure;

namespace EasySql
{
    public class DbContextOptions : Annotations
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IDatabaseProvider DatabaseProvider { get; private set; }

        private readonly DbContextOptionBuilder _optionBuilder;

        public DbContextOptions(DbContextOptionBuilder optionBuilder)
        {
            _optionBuilder = optionBuilder;
        }

        public void SetServiceProvider(IServiceProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            ServiceProvider = provider;
        }

        public void SetDatabaseProvider(IDatabaseProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            DatabaseProvider = provider;
        }

    }
}
