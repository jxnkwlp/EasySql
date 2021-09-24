using System;

namespace EasySql.Databases.TypeMappings
{
    public class GuidTypeMapping : TypeMappingBase
    {
        public GuidTypeMapping() : base(typeof(Guid), System.Data.DbType.Guid)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }
    }
}
