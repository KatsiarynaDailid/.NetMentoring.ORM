using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[EmployeeTerritories]")]
    public class EmployeeTerritory
    {
        [Column("EmployeeID")]
        [NotNull]
        public int EmployeeId { get; set; }

        [Column("TerritoryID")]
        [NotNull]
        public int TerritoryId { get; set; }

        [Association(ThisKey = "EmployeeId", OtherKey = "EmployeeId")]
        public Employee Employee { get; set; }

        [Association(ThisKey = "TerritoryId", OtherKey = "TerritoryId")]
        public Territory Territory { get; set; }
    }
}
