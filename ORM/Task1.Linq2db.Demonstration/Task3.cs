using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task1.Linq2db.Model;

namespace Task1.Linq2db.Demonstration
{
    [TestClass]
    public class Task3
    {
        private Northwind dbNorthwind;

        [TestInitialize]
        public void TestInitialize()
        {
            dbNorthwind = new Northwind();
        }

        /*Добавить нового сотрудника, и указать ему список территорий, за которые он несет ответственность.*/
        [TestMethod]
        public void Task3_1()
        {
            Employee employee = new Employee { FirstName = "Kate", LastName = "Dailid" };

            try
            {
                dbNorthwind.BeginTransaction();

                employee.EmployeeId = Convert.ToInt32(dbNorthwind.InsertWithIdentity(employee));
                dbNorthwind.Territories.Where(t => t.Region.RegionDescription == "Eastern")
                    .Insert(dbNorthwind.EmployeeTerritories, t => new EmployeeTerritory { EmployeeId = employee.EmployeeId, TerritoryId = t.TerritoryId });

                dbNorthwind.CommitTransaction();
            }
            catch
            {
                dbNorthwind.RollbackTransaction();
            }
        }

        /*Перенести продукты из одной категории в другую*/
        [TestMethod]
        public void Task3_2()
        {
            dbNorthwind.Products.Update(p => p.CategoryId == 3, pNew => new Product { CategoryId = 1 });
        }

        /*Добавить список продуктов со своими поставщиками и категориями (массовое занесение),
         * при этом если поставщик или категория с таким названием есть, то использовать их – иначе создать новые.*/
        [TestMethod]
        public void Task3_3()
        {
            var products = new List<Product>() { };
            products.Add(
                new Product
                {
                    ProductName = "Apple",
                    Supplier = new Supplier { CompanyName = "Fruits from Joe" },
                    Category = new Category { CategoryName = "Organic food" }
                });

            products.Add(
                new Product
                {
                    ProductName = "Banana",
                    Supplier = new Supplier { CompanyName = "Fruits from Joe" },
                    Category = new Category { CategoryName = "Organic food" }
                });

            try
            {
                dbNorthwind.BeginTransaction();

                foreach (var product in products)
                {
                    var supplier = dbNorthwind.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);
                    if (supplier == null)
                    {
                        Supplier newSupplier = new Supplier { CompanyName = product.Supplier.CompanyName };
                        product.SupplierId = Convert.ToInt32(dbNorthwind.InsertWithIdentity(newSupplier));
                    }
                    else
                    {
                        product.SupplierId = supplier.SupplierId;
                    }

                    var category = dbNorthwind.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                    if (category == null)
                    {
                        Category newCategory = new Category { CategoryName = product.Category.CategoryName };
                        product.CategoryId = Convert.ToInt32(dbNorthwind.InsertWithIdentity(newCategory));
                    }
                    else
                    {
                        product.CategoryId = category.CategoryId;
                    }
                }

                dbNorthwind.BulkCopy(products);
                dbNorthwind.CommitTransaction();
            }
            catch
            {
                dbNorthwind.RollbackTransaction();
            }
        }


        /*Замена продукта на аналогичный: во всех еще неисполненных заказах 
         * (считать таковыми заказы, у которых ShippedDate = NULL) заменить один продукт на другой.*/
        [TestMethod]
        public void Task3_4()
        {         
            var orderDetailsNullDate = dbNorthwind.OrderDetails.LoadWith(od => od.Order).Where(od => od.Order.ShippedDate == null).ToList();

            foreach (var orderDetailsCurrent in orderDetailsNullDate)
            {
                dbNorthwind.OrderDetails.LoadWith(od => od.Product).Update(od => od.OrderId == orderDetailsCurrent.OrderId && od.ProductId == orderDetailsCurrent.ProductId,
                    od => new OrderDetail
                    {
                        ProductId = dbNorthwind.Products.First(p => !dbNorthwind.OrderDetails.Where(t => t.OrderId == od.OrderId)
                                .Any(t => t.ProductId == p.ProductId) && p.CategoryId == od.Product.CategoryId).ProductId
                    });
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            dbNorthwind.Dispose();
        }
    }
}
