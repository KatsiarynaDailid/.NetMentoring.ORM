using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Territories]")]
    public class Territory
    {
        [Column("TerritoryID")]
        [PrimaryKey]
        [Identity]
        public int TerritoryId { get; set; }

        [Column]
        [NotNull]
        public string TerritoryDescription { get; set; }

        [Column("RegionID")]
        [NotNull]
        public int RegionId { get; set; }

        [Association(ThisKey = "RegionId", OtherKey = "RegionId")]
        public Region Region { get; set; }
    }
}
