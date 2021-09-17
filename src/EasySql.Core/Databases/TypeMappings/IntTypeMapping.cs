namespace EasySql.Databases.TypeMappings
{
    public class IntTypeMapping : TypeMappingBase
    {
        public IntTypeMapping() : base(typeof(int), System.Data.DbType.Int32)
        {
        }
    }
}
