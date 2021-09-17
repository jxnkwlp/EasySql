using System;

namespace EasySql.Infrastructure
{
    public interface IEntityConfigurationLoader
    {
        EntityDefintion Load(Type type);
    }
}
