using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Linq2db.Model
{
    public class Northwind : DataConnection
    {
        public Northwind() : base("Northwind") { }

        public ITable<Category> Categories { get { return GetTable<Category>(); } }

        public ITable<Product> Products { get { return GetTable<Product>(); } }

        public ITable<Supplier> Suppliers { get { return GetTable<Supplier>(); } }

        public ITable<Order> Orders { get { return GetTable<Order>(); } }

        public ITable<OrderDetail> OrderDetails { get { return GetTable<OrderDetail>(); } }

        public ITable<Region> Regions { get { return GetTable<Region>(); } }

        public ITable<Territory> Territories { get { return GetTable<Territory>(); } }

        public ITable<EmployeeTerritory> EmployeeTerritories { get { return GetTable<EmployeeTerritory>(); } }

        public ITable<Employee> Employees { get { return GetTable<Employee>(); } }

        public ITable<Shipper> Shippers { get { return GetTable<Shipper>(); } }

    }
}
