using System;
using System.Collections.Generic;

namespace EasySql.Databases.TypeMappings
{
    public class TypeMappingConfiguration : ITypeMappingConfiguration
    {
        private readonly Dictionary<Type, ITypeMapping> typeMappings = new Dictionary<Type, ITypeMapping>()
        {
            { typeof(bool), new BoolTypeMapping() },
            { typeof(DateTimeOffset), new DateTimeOffsetTypeMapping() },
            { typeof(DateTime), new DateTimeTypeMapping() },
            { typeof(decimal), new DecimalTypeMapping() },
            { typeof(double), new DoubleTypeMapping() },
            { typeof(float), new FloatTypeMapping() },
            { typeof(Guid), new GuidTypeMapping() },
            { typeof(int), new IntTypeMapping() },
            { typeof(string), new StringTypeMapping() },
        };

        public virtual ITypeMapping FindMapping(Type type)
        {
            if (typeMappings.ContainsKey(type))
                return typeMappings[type];

            // TODO 
            return null;
        }
    }
}
