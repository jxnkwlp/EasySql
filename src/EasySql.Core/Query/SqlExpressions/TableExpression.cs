namespace EasySql.Query.SqlExpressions
{
    public class TableExpression : SqlExpression
    {
        public string Schame { get; }
        public string Alias { get; }
        public string Name { get; }

        public TableExpression(string schame, string alias, string name)
        {
            Schame = schame;
            Alias = alias;
            Name = name;
        }

    }
}
