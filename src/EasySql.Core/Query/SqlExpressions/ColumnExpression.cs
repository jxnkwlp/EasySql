namespace EasySql.Query.SqlExpressions
{
    public class ColumnExpression : SqlExpression
    {
        public ColumnExpression(string alias, string name)
        {
            Alias = alias;
            Name = name;
        }

        public string Alias { get; }

        public string Name { get; }

    }
}
