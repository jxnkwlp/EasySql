namespace EasySql.Databases.TypeMappings
{
    public class StringTypeMapping : TypeMappingBase
    {
        public StringTypeMapping() : base(typeof(string), System.Data.DbType.String)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }
    }
}
