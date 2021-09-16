using System;

namespace EasySql.Query.SqlExpressions
{
    public class SqlFragmentExpression : SqlExpression
    {
        public string Sql { get; }

        public override Type Type => typeof(string);

        public SqlFragmentExpression(string sql)
        {
            Sql = sql;
        }
    }
}
