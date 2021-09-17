using System;

namespace EasySql.Infrastructure
{
    public interface IEntityConfiguration
    {
        EntityDefintion RegisterEntity(Type type);

        EntityDefintion FindEntity(Type type);
    }
}
