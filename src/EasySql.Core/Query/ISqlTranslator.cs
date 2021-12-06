using EasySql.Databases;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query
{
    public interface ISqlTranslator
    {
        IDatabaseCommand CreateDatabaseCommand(SqlExpression expression);
    }
}
