using System.Collections.Generic;

namespace EasySql.Query.SqlExpressions
{
    public class SqlFunctionExpression : SqlExpression
    {
        public SqlFunctionExpression(string schema, string name, IList<SqlExpression> arguments = null, bool nullable = false)
        {
            Schema = schema;
            Name = name;
            Arguments = arguments;
            Nullable = nullable;
        }

        public string Schema { get; }

        public string Name { get; }

        public IList<SqlExpression> Arguments { get; }

        public bool Nullable { get; }
    }
}
