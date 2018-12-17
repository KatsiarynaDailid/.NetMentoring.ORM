using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Suppliers]")]
    public class Supplier
    {       
        [Column("SupplierID")]
        [PrimaryKey]
        [Identity]
        public int SupplierId { get; set; }

        [Column]
        [NotNull]
        public string CompanyName { get; set; }

        [Column]
        public string ContactName { get; set; }

        [Association(ThisKey = "SupplierId", OtherKey = "SupplierId", CanBeNull = true)]
        public IEnumerable<Product> Products { get; set; }
    }
}
