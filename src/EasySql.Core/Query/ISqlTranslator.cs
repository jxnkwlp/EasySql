using EasySql.Databases;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query
{
    public interface ISqlTranslator
    {
        ISqlCommandBuilder Translate(SqlExpression expression);
    }
}
