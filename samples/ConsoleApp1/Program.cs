using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EasySql;
using EasySql.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine();

            //  
            IServiceCollection services = new ServiceCollection();

            //services.AddSingleton<IEntityConfiguration, EntityConfiguration>();
            //services.AddSingleton<IEntityConfigurationLoader, EntityConfigurationLoader>();
            //services.AddSingleton<ITypeMappingConfiguration, TypeMappingConfiguration>();

            //var s = services.BuildServiceProvider();

            //var a = s.GetService<IEntityConfiguration>();

            //services.AddDbContext();

            Console.WriteLine();
            // 

            var dbContext = new DbContext(b =>
            {
                b.UseSqlServer("server=.;database=Northwind;uid=sa;password=Pass@123456");
                b.Services.AddLogging(x => x.AddConsole());
            });

            var query = dbContext.Query<Order>();
            var query2 = dbContext.Query<Product>();
            var now = DateTime.Now;

            // Console.WriteLine(query.Where(x => x.ShipName == null).ToSqlText());
            // Console.WriteLine(query.Where(x => x.ShipName != "a").ToSqlText());
            //  Console.WriteLine(query.Where(x => x.Id > p1).ToSqlText());
            // Console.WriteLine(query2.Where(x => x.Discontinued == true).ToSqlText());
            // Console.WriteLine(query2.Where(x => x.Discontinued).ToSqlText());
            // Console.WriteLine(query.Where(x => x.RequiredDate >= now && x.CustomerID != null).ToSqlText());

            foreach (var item in query.ToArray())
            {
                Console.WriteLine($"Id: {item.Id}");
            }

            // Console.WriteLine(query.Any());

            // Console.WriteLine(query.Count(x => !string.IsNullOrEmpty(x.CustomerID)));

            // query.Count(x => x.ShipName.Contains("a")).ToList();
            // query.Where(x => x.ShipName.StartsWith("a")).ToList();
            // query.Where(x => x.ShipName.EndsWith("a")).ToList();
            // query.Where(x => x.OrderDate).ToList();

            //ParameterExpression paramExpr = Expression.Parameter(typeof(int), "arg");

            //LambdaExpression lambdaExpr = Expression.Lambda<Func<int, int>>(
            //    Expression.Add(
            //        paramExpr,
            //        Expression.Constant(1)
            //    ),
            //    new List<ParameterExpression>() { paramExpr }
            //);

            //// Print out the expression.
            //Console.WriteLine(lambdaExpr);

            //// Compile and run the lamda expression.
            //// The value of the parameter is 1.
            //Console.WriteLine(lambdaExpr.Compile().DynamicInvoke());

            // query.ToList();

            // query.Where(x => x.Id <= 10).ToList();
            // query.Where(x => (x.Id <= 10 || x.Id > 30) && x.Age > 50).ToList();
            // query.Where(x => x.Name.Contains("a")).ToList();
            // query.Where(x => !x.Name.Contains("a")).ToList();
            // query.Where(x => x.Name.Contains("a") && x.Id == 10).ToList();
            // query.Where(x => (x.Id <= 10 || x.Id > 30) && x.Name.Contains("a")).ToList();

            //query.All(x => x.Id > 0);
            //query.Any();
            //query.Any(x => x.Id > 0);
            //query.Average(x => x.Id);
            //query.Max(x => x.Id);
            //query.Min(x => x.Id);
            //query.Sum(x => x.Id);
            // query.Count();
            //query.Count(x => x.Id > 0);

            // query.Contains(new Student());
            //query.First(x => x.Id > 0);
            //query.First();

            // query.Select(x => new { x.OrderID }).ToList();
            // query.Select(x => x.OrderID).ToList();

            // query.GroupBy(x => x.Name).ToList();
            // query.GroupBy(x => new { x.CustomerID, x.EmployeeID }).Select(x => new { a = x.Key, b = x.Max(c => c.OrderDate) }).ToList();


        }
    }

    public class TestResult<T> : IEnumerable<T>
    {
        public TestResult()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)new List<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
    }

    [Table("Orders")]
    public class Order
    {
        [Column("OrderID")]
        public int Id { get; set; }
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public int? ShipVia { get; set; }
        public decimal Freight { get; set; }

    }

    [Table("Products")]
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public bool Discontinued { get; set; }
    }
}
