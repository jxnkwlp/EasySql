using EasySql.Infrastructure;

namespace EasySql.Query.SqlExpressions
{
    public class ColumnExpression : SqlExpression
    {
        public string Name { get; }
        public string Alias { get; }
        public string TableAlias { get; }
        public ColunmnDefintion Colunmn { get; }

        public ColumnExpression(ColunmnDefintion colunmn, string alias, string tableAlias) : this(colunmn.Name, alias, tableAlias, colunmn)
        {
            Alias = alias;
            TableAlias = tableAlias;
        }

        public ColumnExpression(string name, string alias, string tableAlias, ColunmnDefintion colunmn)
        {
            Name = name;
            Alias = alias;
            TableAlias = tableAlias;
            Colunmn = colunmn;
        }
    }
}
