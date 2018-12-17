using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Orders]")]
    public class Order
    {
        [Column("OrderID")]
        [Identity]
        [PrimaryKey]
        public int OrderId { get; set; }

        [Column("ShipVia")]
        public int? ShipperId { get; set; }

        [Column]
        public DateTime? ShippedDate { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        [Association(ThisKey = "EmployeeId", OtherKey = "EmployeeId", CanBeNull = true)]
        public Employee Employee { get; set; }

        [Association(ThisKey = "ShipperId", OtherKey = "ShipperId", CanBeNull = true)]
        public Shipper Shipper { get; set; }

    }
}
