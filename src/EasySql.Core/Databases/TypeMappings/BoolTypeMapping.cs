namespace EasySql.Databases.TypeMappings
{
    public class BoolTypeMapping : TypeMappingBase
    {
        public BoolTypeMapping() : base(typeof(bool), System.Data.DbType.Boolean)
        {
        }
    }
}
