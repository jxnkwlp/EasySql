using System.Linq;
using EasySql.Tests.Entities;
using NUnit.Framework;

namespace EasySql.Tests
{
    public class QueryResultTests : TestBase
    {
        [Test]
        public void Test_1()
        {
            var query = DbContext.Query<Order>();

            var count = query.Count();

            Assert.IsTrue(count == 830);
        }

    }
}
