using System;
using System.Linq;
using EasySql.Query;
using EasySql.Query.SqlExpressions;
using EasySql.SqlServer;
using EasySql.Tests.Entities;
using NUnit.Framework;

namespace EasySql.Tests
{
    public class QueryTranslator
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

        private QueryExpression ToQueryExpression(IQueryable queryable)
        {
            if (queryable.Provider is EntityQueryProvider entityQueryProvider)
            {
                var queryContext = entityQueryProvider.QueryContext;

                var executor = new QueryExecutor(queryContext);

                return executor.Translate(queryable.Expression) as QueryExpression;
            }
            else
            {
                Assert.Fail();
                return null;
            }
        }

        [Test]
        public void Expression_Projection_1()
        {
            var query = _dbContext.Query<Order>().Where(x => x.Id > 0);

            var expression = ToQueryExpression(query);
            Assert.NotNull(expression);

            var binaryExpression = expression.Predicate as SqlBinaryExpression;
            Assert.NotNull(binaryExpression);

            Assert.IsTrue(binaryExpression.Left is ColumnExpression);
            Assert.IsTrue(binaryExpression.Right is SqlConstantExpression);
        }

        [Test]
        public void Expression_Projection_2()
        {
            var query = _dbContext.Query<Order>().Where(x => x.OrderDate != null);

            var expression = ToQueryExpression(query);
            Assert.NotNull(expression);

            var unaryExpression = expression.Predicate as SqlUnaryExpression;
            Assert.NotNull(unaryExpression);

            Assert.IsTrue(unaryExpression.Operand is ColumnExpression);
        }

        [Test]
        public void Expression_Projection_3()
        {
            var now = DateTime.Now;

            var query = _dbContext.Query<Order>().Where(x => x.OrderDate <= now);

            var expression = ToQueryExpression(query);
            Assert.NotNull(expression);

            var binaryExpression = expression.Predicate as SqlBinaryExpression;
            Assert.NotNull(binaryExpression);

            Assert.IsTrue(binaryExpression.Left is ColumnExpression);
            Assert.IsTrue(binaryExpression.Right is SqlConstantExpression);
        }

        [Test]
        public void Expression_Projection_4()
        {
            var query = _dbContext.Query<Product>().Where(x => x.Discontinued);

            var expression = ToQueryExpression(query);
            Assert.NotNull(expression);

            var binaryExpression = expression.Predicate as SqlBinaryExpression;
            Assert.NotNull(binaryExpression);

            Assert.IsTrue(binaryExpression.Left is ColumnExpression);
            Assert.IsTrue(binaryExpression.Right is SqlConstantExpression);
        }
    }
}
