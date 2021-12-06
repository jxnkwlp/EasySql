using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasySql.Tests.Entities
{
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
}
