using EasySql.SqlServer;
using NUnit.Framework;
namespace EasySql.Tests
{
    public abstract class TestBase
    {
        protected DbContext DbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            DbContext = new DbContext(b =>
            {
                b.UseSqlServer("server=.;database=Northwind;uid=sa;password=Pass@123456");
            });
        }

    }
}
