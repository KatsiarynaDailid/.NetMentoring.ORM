using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Order Details]")]
    public class OrderDetail
    {
        [Column("OrderID")]
        [PrimaryKey]
        public int OrderId { get; set; }

        [Column("ProductID")]
        [PrimaryKey]
        public int ProductId { get; set; }

        [Association(ThisKey = "ProductId", OtherKey = "ProductId")]
        public Product Product { get; set; }

        [Association(ThisKey = "OrderId", OtherKey = "OrderId")]
        public Order Order { get; set; }
    }
}
