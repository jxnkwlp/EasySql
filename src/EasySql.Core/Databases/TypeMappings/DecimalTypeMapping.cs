namespace EasySql.Databases.TypeMappings
{
    public class DecimalTypeMapping : TypeMappingBase
    {
        public DecimalTypeMapping() : base(typeof(decimal), System.Data.DbType.Decimal)
        {

        }
    }
}
