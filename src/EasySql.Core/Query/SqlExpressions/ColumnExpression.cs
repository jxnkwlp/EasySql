using EasySql.Infrastructure;

namespace EasySql.Query.SqlExpressions
{
    public class ColumnExpression : SqlExpression
    {
        public string Name { get; }
        public string Alias { get; }
        public string TableAlias { get; }
        public bool IsNullable { get; }
        public ColunmnDefintion Colunmn { get; }

        public ColumnExpression(ColunmnDefintion colunmn, string alias, string tableAlias)
        {
            Colunmn = colunmn;
            Alias = alias;
            TableAlias = tableAlias;
            Name = colunmn.Name;
            IsNullable = colunmn.IsNullable;
        }

    }
}
