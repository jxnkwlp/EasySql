using System;
using System.Data;

namespace EasySql.Databases.TypeMappings
{
    public abstract class TypeMappingBase : ITypeMapping
    {
        public TypeMappingBase(Type clrType, DbType? dbType = null, int? size = null, int? precision = null, bool fixedLength = false)
        {
            ClrType = clrType;
            DbType = dbType;
            Size = size;
            Precision = precision;
            FixedLength = fixedLength;
        }

        public virtual Type ClrType { get; }

        public virtual DbType? DbType { get; }

        public virtual int? Size { get; }

        public virtual int? Precision { get; }

        public virtual bool FixedLength { get; }

        public virtual string GetConstantLiteral(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToString();
        }
    }

    public abstract class TypeMappingBase<T> : TypeMappingBase, ITypeMapping<T>
    {
        public TypeMappingBase(Type clrType, DbType? dbType = null, int? size = null, int? precision = null, bool fixedLength = false) : base(clrType, dbType, size, precision, fixedLength)
        {
        }

        public virtual T GetValue(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
