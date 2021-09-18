using System.Linq;
using EasySql.SqlServer;
using EasySql.Tests.Entities;
using NUnit.Framework;

namespace EasySql.Tests
{
    public class SqlTranslatorTests
    {
        private DbContext _dbContext = null;

        [SetUp]
        public void Setup()
        {
            _dbContext = new DbContext(b =>
            {
                b.UseSqlServer("server=.;database=Northwind;uid=sa;password=Pass@123456");
            });

        }

        [Test]
        public void Where_Conditions_1()
        {
            var query = _dbContext.Query<Order>();

            var sql = query.Where(x => x.Id > 10).ToSqlText();

            Assert.IsTrue(sql == @"SELECT ""o"".""OrderID"" , ""o"".""CustomerID"" , ""o"".""EmployeeID"" , ""o"".""OrderDate"" , ""o"".""RequiredDate"" , ""o"".""ShipName""
FROM ""Orders"" AS ""o""
WHERE ""o"".""OrderID"" > 10");

        }
    }
}
