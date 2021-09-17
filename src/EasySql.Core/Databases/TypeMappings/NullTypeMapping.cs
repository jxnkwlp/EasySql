using System;
using System.Data;

namespace EasySql.Databases.TypeMappings
{
    public class NullTypeMapping : ITypeMapping
    {
        public Type ClrType => null;

        public DbType? DbType => null;

        public int? Size { get; }

        public int? Precision { get; }

        public bool FixedLength { get; }
    }
}
