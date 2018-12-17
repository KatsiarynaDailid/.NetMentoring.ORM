using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    [Table("[Northwind].[Employees]")]
    public class Employee
    {
        [Column("EmployeeID")]
        [PrimaryKey]
        [Identity]
        public int EmployeeId { get; set; }

        [Column]
        public string LastName { get; set; }

        [Column]
        public string FirstName { get; set; }
    }
}
