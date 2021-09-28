using System;
using Microsoft.Extensions.DependencyInjection;

namespace EasySql.Infrastructure
{
    public static class Extensions
    {
        public static Type GetUnderlyingType(this Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
                return Nullable.GetUnderlyingType(type);
            return type;
        }

        public static T GetRequiredService<T>(this DbContextOptions options) where T : class
        {
            return options.ServiceProvider.GetRequiredService<T>();
        }
    }
}
