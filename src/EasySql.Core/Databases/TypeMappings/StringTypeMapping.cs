namespace EasySql.Databases.TypeMappings
{
    public class StringTypeMapping : TypeMappingBase<string>
    {
        public StringTypeMapping() : base(typeof(string), System.Data.DbType.String)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }

        public override string GetValue(object value)
        {
            return value?.ToString();
        }
    }
}
