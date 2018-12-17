using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Categories]")]
    public class Category
    {
        [PrimaryKey]
        [Identity]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Column]
        public string CategoryName { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public byte[] Picture { get; set; }

        [Association(ThisKey = "CategoryId", OtherKey = "CategoryId", CanBeNull = true)]
        public IEnumerable<Product> Products { get; set; }
    }
}
