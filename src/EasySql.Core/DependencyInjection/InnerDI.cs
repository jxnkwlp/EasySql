using System;
using Microsoft.MinIoC;

namespace EasySql.DependencyInjection
{
    public class InnerDI : IServiceProvider, IServiceRegistor
    {
        private readonly Container _container = new Container();

        public InnerDI()
        {
        }

        public void AddService<T>(bool isSingleton = false) where T : class
        {
            _container.Register<T>();
        }

        public void AddService<T>(Func<T> instanceFactory, bool isSingleton = false) where T : class
        {
            _container.Register<T>(instanceFactory);
        }

        public object GetService(Type serviceType)
        {
            return _container.GetService(serviceType);
        }

        public void AddService<T, TImpl>(bool isSingleton) where T : class where TImpl : class, T
        {
            _container.Register<T, TImpl>();
        }
    }
}
