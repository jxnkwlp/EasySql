using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySql.Tests.Entities
{
    [Table("Products")]
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public bool Discontinued { get; set; }
    }
}
