using System;

namespace EasySql.Databases.TypeMappings
{
    public class DateTimeOffsetTypeMapping : TypeMappingBase
    {
        public DateTimeOffsetTypeMapping() : base(typeof(DateTimeOffset), System.Data.DbType.DateTime)
        {

        }
    }
}
