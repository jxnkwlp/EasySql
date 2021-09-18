using System;
using System.Diagnostics;
using System.Linq;
using EasySql;
using EasySql.DependencyInjection;
using EasySql.SqlServer;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddDbContext();


            // 

            var dbContext = new DbContext(b =>
            {
                b.UseSqlServer("server=.;database=Northwind;uid=sa;password=Pass@123456");
            });

            var query = dbContext.Query<Order>();


            Console.WriteLine(query.Where(x => x.OrderID > 10).ToSqlText());

            //for (int i = 0; i < 5; i++)
            //{
            //    var sw = Stopwatch.StartNew();

            //    Console.WriteLine($"Count: {query.Count()}");

            //    sw.Stop();

            //    Console.WriteLine(sw.Elapsed);
            //}



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

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string ShipName { get; set; }
    }
}
