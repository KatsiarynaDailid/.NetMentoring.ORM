using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Region]")]
    public class Region
    {
        [Column("RegionID")]
        [Identity]
        [PrimaryKey]
        public int RegionId { get; set; }

        [Column]
        [NotNull]
        public string RegionDescription { get; set; }
    }
}
