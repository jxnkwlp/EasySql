using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySql.Infrastructure
{
    public class EntityDefintion
    {
        public string Name { get; }

        public string Schema { get; }

        public Type EntityType { get; }

        public IReadOnlyList<ColunmnDefintion> Colunmns { get; }

        public EntityDefintion(string name, string schema, Type entityType, IReadOnlyList<ColunmnDefintion> colunmns)
        {
            Name = name;
            Schema = schema;
            EntityType = entityType;
            Colunmns = colunmns;
        }

        public ColunmnDefintion FindColumn(MemberInfo member)
        {
            return Colunmns.FirstOrDefault(x => x.Member == member);
        }
    }
}
