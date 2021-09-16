using System.Data.Common;

namespace EasySql.Databases
{
    public class SqlDataReader : ISqlDataReader
    {
        public SqlDataReader(DbDataReader reader)
        {

        }

        public void Dispose()
        {
        }
    }
}
