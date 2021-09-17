using System;

namespace EasySql.Databases.TypeMappings
{
    public interface ITypeMappingConfiguration
    {
        ITypeMapping FindMapping(Type type);
    }
}
