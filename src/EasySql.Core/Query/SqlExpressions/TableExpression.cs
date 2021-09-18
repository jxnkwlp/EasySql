using EasySql.Infrastructure;

namespace EasySql.Query.SqlExpressions
{
    public class TableExpression : SqlExpression
    {
        public string Schame { get; }
        public string Name { get; }
        public string Alias { get; }

        public EntityDefintion Entity { get; }

        public TableExpression(EntityDefintion entity, string alias) : this(entity.Schema, entity.Name, null, entity)
        {
            Alias = alias;
        }

        public TableExpression(string schame, string name, string alias, EntityDefintion entityDefintion)
        {
            Schame = schame;
            Name = name;
            Alias = alias;
            Entity = entityDefintion;
        }

    }
}
