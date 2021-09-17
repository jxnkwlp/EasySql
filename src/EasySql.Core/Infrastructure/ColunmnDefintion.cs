using System;
using EasySql.Databases.TypeMappings;

namespace EasySql.Infrastructure
{
    public class ColunmnDefintion
    {
        public ColunmnDefintion(Type type, string name, bool isKey, bool isNullable, int? length, ITypeMapping mapping)
        {
            Type = type;
            Name = name;
            IsKey = isKey;
            IsNullable = isNullable;
            Length = length;
            Mapping = mapping;
        }

        public Type Type { get; }
        public string Name { get; }
        public bool IsKey { get; }
        public bool IsNullable { get; }
        public int? Length { get; }
        private ITypeMapping Mapping { get; }
    }
}
