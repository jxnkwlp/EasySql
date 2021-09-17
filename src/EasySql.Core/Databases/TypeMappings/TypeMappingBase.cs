using System;
using System.Data;

namespace EasySql.Databases.TypeMappings
{
    public abstract class TypeMappingBase : ITypeMapping
    {
        protected TypeMappingBase(Type clrType, DbType? dbType = null, int? size = null, int? precision = null, bool fixedLength = false)
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
    }
}
