namespace EasySql.Databases.TypeMappings
{
    public class FloatTypeMapping : TypeMappingBase
    {
        public FloatTypeMapping() : base(typeof(float), System.Data.DbType.Single)
        {
        }
    }
}
