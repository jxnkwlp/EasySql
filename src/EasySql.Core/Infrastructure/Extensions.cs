using System;

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
    }
}
