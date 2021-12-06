using System;

namespace EasySql.Databases.TypeMappings
{
    public class GuidTypeMapping : TypeMappingBase<Guid>
    {
        public GuidTypeMapping() : base(typeof(Guid), System.Data.DbType.Guid)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }

        public override Guid GetValue(object value)
        {
            if (value == null)
                throw new Exception("The value is null, can't be convert to Guid.");

            return Guid.Parse(value.ToString());
        }
    }

    public class GuidNullableTypeMapping : TypeMappingBase<Guid?>
    {
        public GuidNullableTypeMapping() : base(typeof(Guid?), System.Data.DbType.Guid)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }

        public override Guid? GetValue(object value)
        {
            if (value == null)
                return null;

            return Guid.Parse(value.ToString());
        }
    }

    public class GuidToStringMapping : TypeMappingBase<Guid>
    {
        public GuidToStringMapping() : base(typeof(Guid), System.Data.DbType.String)
        {
        }

        public override string GetConstantLiteral(object value)
        {
            return $"'{value}'";
        }

        public override Guid GetValue(object value)
        {
            if (value == null)
                throw new Exception("The value is null, can't be convert to Guid.");

            return Guid.Parse(value.ToString());
        }
    }
}
