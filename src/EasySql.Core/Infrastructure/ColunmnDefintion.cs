using System;
using System.Reflection;
using EasySql.Databases.TypeMappings;

namespace EasySql.Infrastructure
{
    public class ColunmnDefintion
    {
        public ColunmnDefintion(MemberInfo member, Type clrType, string name, bool isKey, bool isNullable, int? length, ITypeMapping mapping)
        {
            Member = member;
            ClrType = clrType;
            Name = name;
            IsKey = isKey;
            IsNullable = isNullable;
            Length = length;
            Mapping = mapping;
        }

        public MemberInfo Member { get; set; }
        public Type ClrType { get; }
        public string Name { get; }
        public bool IsKey { get; }
        public bool IsNullable { get; }
        public int? Length { get; }
        private ITypeMapping Mapping { get; }
    }
}
