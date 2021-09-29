using System.Collections.Generic;
using System.Linq.Expressions;

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

        public SqlFunctionExpression(Expression operand, string schema, string name, IList<SqlExpression> arguments, bool nullable = false)
        {
            Operand = operand;
            Schema = schema;
            Name = name;
            Arguments = arguments;
            Nullable = nullable;
        }

        public Expression Operand { get; }

        public string Schema { get; }

        public string Name { get; }

        public IList<SqlExpression> Arguments { get; }

        public bool Nullable { get; }

    }
}
