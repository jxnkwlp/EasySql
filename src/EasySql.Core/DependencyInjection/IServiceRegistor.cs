using System;

namespace EasySql.DependencyInjection
{
    public interface IServiceRegistor
    {
        void AddService<T>(bool isSingleton = false) where T : class;
        void AddService<T>(Func<T> instanceFactory, bool isSingleton = false) where T : class;
        void AddService<T, TImpl>(bool isSingleton = false) where T : class where TImpl : class, T;
    }
}
