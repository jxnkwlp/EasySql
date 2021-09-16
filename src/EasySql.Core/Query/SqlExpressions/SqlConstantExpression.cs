using System;

namespace EasySql.Query.SqlExpressions
{
    public class SqlConstantExpression : SqlExpression
    {
        public object Value { get; }

        public override Type Type => Value.GetType();

        public SqlConstantExpression(object value)
        {
            Value = value;
        }
    }
}
