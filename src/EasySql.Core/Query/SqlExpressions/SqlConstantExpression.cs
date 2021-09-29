using System;
using System.Linq.Expressions;

namespace EasySql.Query.SqlExpressions
{
    public class SqlConstantExpression : SqlExpression
    {
        public object Value { get; }

        public override Type Type => Value?.GetType();

        public SqlConstantExpression(object value)
        {
            Value = value;
        }

        public Expression Update(object value)
        {
            return value == Value ? this : new SqlConstantExpression(value);
        }

        public string GetConstantLiteralString()
        {
            if (Value == null)
            {
                return "NULL";
            }

            if (Type == typeof(bool))
            {
                if ((bool)Value)
                    return "1";
                else
                    return "0";
            }
            else if (Type == typeof(string))
            {
                return $"'{Value}'";
            }

            return Value?.ToString();
        }
    }
}
