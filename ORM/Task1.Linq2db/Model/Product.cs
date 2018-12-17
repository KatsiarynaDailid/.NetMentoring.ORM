using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Products]")]
    public class Product
    {
        [Column("ProductID")]
        [Identity]
        [PrimaryKey]
        public int ProductId { get; set; }

        [Column]
        public string ProductName { get; set; }

        [Association(ThisKey = "CategoryId", OtherKey = "CategoryId")]
        public Category Category { get; set; }

        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Association(ThisKey = "SupplierId", OtherKey = "SupplierId", CanBeNull = true)]
        public Supplier Supplier { get; set; }

        [Column("SupplierID")]
        public int SupplierId { get; set; }

        [Column]
        public string QuantityPerUnit { get; set; }

        [Column]
        public decimal? UnitPrice { get; set; }

        [Column]
        public int? UnitsInStock { get; set; }

        [Column]
        public int? UnitsOnOrder { get; set; }

        [Column]
        public int? ReorderLevel { get; set; }

        [Column]
        public bool Discontinued { get; set; }
    }
}
