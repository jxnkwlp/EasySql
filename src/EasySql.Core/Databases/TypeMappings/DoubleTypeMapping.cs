namespace EasySql.Databases.TypeMappings
{
    public class DoubleTypeMapping : TypeMappingBase
    {
        public DoubleTypeMapping() : base(typeof(double), System.Data.DbType.Double)
        {
        }
    }
}
