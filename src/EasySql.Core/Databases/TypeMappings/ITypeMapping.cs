using System;
using System.Data;

namespace EasySql.Databases.TypeMappings
{
    public interface ITypeMapping
    {
        Type ClrType { get; }
        DbType? DbType { get; }
        int? Size { get; }
        int? Precision { get; }
        bool FixedLength { get; }

        string GetConstantLiteral(object value);
    }

    public interface ITypeMapping<T> : ITypeMapping
    {
        T GetValue(object value);
    }

}
