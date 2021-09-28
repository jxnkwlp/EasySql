//using System;

//namespace EasySql.DependencyInjection
//{
//    public static class Extensions
//    {
//        public static T GetService<T>(this IServiceProvider serviceProvider) where T : class
//        {
//            return (T)serviceProvider.GetService(typeof(T));
//        }

//        public static T GetRequiredService<T>(this IServiceProvider serviceProvider) where T : class
//        {
//            var service = (T)serviceProvider.GetService(typeof(T));

//            if (service == null) throw new Exception($"The service of '{typeof(T).FullName}' can't be resolved.");

//            return service;
//        }
//    }
//}
