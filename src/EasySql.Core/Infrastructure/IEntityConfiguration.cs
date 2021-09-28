using System;

namespace EasySql.Infrastructure
{
    public interface IEntityConfiguration
    {
        EntityDefintion Register(Type type);

        EntityDefintion Find(Type type);
    }
}
