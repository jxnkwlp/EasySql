using System;

namespace EasySql.DependencyInjection
{
    public interface IServiceProviderAccessor
    {
        IServiceProvider GetServiceProvider();
    }
}
