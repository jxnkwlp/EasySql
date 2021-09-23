namespace EasySql.Databases.TypeMappings
{
    public class BoolTypeMapping : TypeMappingBase
    {
        public BoolTypeMapping() : base(typeof(bool), System.Data.DbType.Boolean)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return (bool)value ? "1" : "0";
        }
    }
}
