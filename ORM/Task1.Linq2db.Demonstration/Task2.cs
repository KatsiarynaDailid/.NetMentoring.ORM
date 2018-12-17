using System;
using LinqToDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1.Linq2db.Model;
using System.Linq;

namespace Task1.Linq2db.Demonstration
{
    [TestClass]
    public class Task2
    {
        private Northwind dbNorthwind;

        [TestInitialize]
        public void TestInitialize()
        {
            dbNorthwind = new Northwind();
        }

        /*
         Список продуктов с категорией и поставщиком
         */
        [TestMethod]
        public void Task1_1()
        {
            foreach(var product in dbNorthwind.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToList())
            {
                Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category?.CategoryName}, Supplier: {product.Supplier?.ContactName}.");
            }        
        }

        /*Список сотрудников с указанием региона, за который они отвечают*/
        [TestMethod]
        public void Task1_2()
        {
            var resultQuery = (from e in dbNorthwind.Employees
                        join et in dbNorthwind.EmployeeTerritories on e.EmployeeId equals et.EmployeeId into etList
                        from etCurrent in etList.DefaultIfEmpty()
                        join t in dbNorthwind.Territories on etCurrent.TerritoryId equals t.TerritoryId into tList
                        from tCurrent in tList.DefaultIfEmpty()
                        join r in dbNorthwind.Regions on tCurrent.RegionId equals r.RegionId into rList
                        from rCurrent in rList.DefaultIfEmpty()
                        select new { Employee = e, Region = rCurrent }).Distinct();

            foreach (var record in resultQuery)
            {
                Console.WriteLine($"Employee: {record.Employee.FirstName} {record.Employee.LastName}, Region: {record.Region.RegionDescription}.");
            }
        }

        /*Статистики по регионам: количества сотрудников по регионам*/
        [TestMethod]
        public void Task1_3()
        {
            var getListOfRegionsAndEmployees = from r in dbNorthwind.Regions
                        join t in dbNorthwind.Territories on r.RegionId equals t.RegionId into tList
                        from tCurrent in tList.DefaultIfEmpty()
                        join et in dbNorthwind.EmployeeTerritories on tCurrent.TerritoryId equals et.TerritoryId into etList
                        from etCurrent in etList.DefaultIfEmpty()
                        join e in dbNorthwind.Employees on etCurrent.EmployeeId equals e.EmployeeId into eList
                        from eCurrent in eList.DefaultIfEmpty()
                        select new { Region = r, eCurrent.EmployeeId };

            var resultQuery = from current in getListOfRegionsAndEmployees.Distinct()
                         group current by current.Region into grResult
                         select new { grResult.Key.RegionDescription, EmployeesCount = grResult.Count(e => e.EmployeeId != 0) };

            foreach(var record in resultQuery)
            {
                Console.WriteLine($"Region: {record.RegionDescription}; Count of Employees: {record.EmployeesCount}.");
            }
        }

        /*Списка «сотрудник – с какими грузоперевозчиками работал» (на основе заказов)*/
        [TestMethod]
        public void Task1_4()
        {
            var resultQuery = (from e in dbNorthwind.Employees
                         join o in dbNorthwind.Orders on e.EmployeeId equals o.EmployeeId into oList
                         from oCurrent in oList.DefaultIfEmpty()
                         join s in dbNorthwind.Shippers on oCurrent.ShipperId equals s.ShipperId into sList
                         from sCurrent in sList.DefaultIfEmpty()
                         select new { e.EmployeeId, e.FirstName, e.LastName, sCurrent.CompanyName }).Distinct().OrderBy(t => t.EmployeeId).ToList();

            foreach (var record in resultQuery)
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName}; Shipper: {record.CompanyName}");
            }

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            dbNorthwind.Dispose();
        }
    }
}
 