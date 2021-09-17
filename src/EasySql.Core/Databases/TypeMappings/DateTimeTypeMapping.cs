using System;

namespace EasySql.Databases.TypeMappings
{
    public class DateTimeTypeMapping : TypeMappingBase
    {
        public DateTimeTypeMapping() : base(typeof(DateTime), System.Data.DbType.DateTime)
        {
        }
    }
}
