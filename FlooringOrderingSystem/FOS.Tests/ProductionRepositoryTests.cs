using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FOS.Data.Production;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Tests
{
    [TestFixture]
    public class ProductionRepositoryTests
    {
        [Test]
        public void CanRetrieveProductionTaxData()
        {
            ITaxRepository data = new ProductionTaxesRepository(@"C:\Data\LiveData\Taxes.txt");

            List<Tax> taxes = data.RetrieveTaxes();

            Assert.IsTrue(taxes.Count > 0);
        }

        [Test]
        public void CanRetrieveProductionProductData()
        {
            IProductRepository data = new ProductionProductsRepository(@"C:\Data\LiveData\Products.txt");

            List<Product> products = data.RetrieveProducts();

            Assert.IsTrue(products.Count > 0);
        }

        [Test]
        public void CanCreateProductionOrder()
        {
            IOrderRepository data = new ProductionOrdersRepository(@"C:\Data\LiveData");

            Order order = new Order();
            order.OrderDate = DateTime.Parse("01/01/1111");

            Assert.AreEqual(true, data.CreateOrder(order));
        }

        [Test]
        public void CanRetrieveProductionOrder()
        {
            IOrderRepository data = new ProductionOrdersRepository(@"C:\Data\LiveData");

            Order order = new Order();
            order.OrderDate = DateTime.Parse("01/01/1111");
            order.CustomerName = "Thommas";

            data.CreateOrder(order);

            Order retrieved = data.RetrieveOrder(order);

            Assert.AreEqual(order.CustomerName, retrieved.CustomerName);
        }

        [Test]
        public void CanRetrieveProductionOrdersByDate()
        {
            IOrderRepository data = new ProductionOrdersRepository(@"C:\Data\LiveData");
            DateTime date = DateTime.Parse("01/01/1111");

            List<Order> orders = data.RetrieveOrdersByDate(date);

            Assert.IsTrue(orders.Count > 1);
        }

        [Test]
        public void CanUpdateProductionOrder()
        {
            IOrderRepository data = new ProductionOrdersRepository(@"C:\Data\LiveData");

            Order order = new Order();
            order.OrderDate = DateTime.Parse("01/01/1111");
            order.OrderNumber = 3;
            order.CustomerName = "Thomas!";

            Order retrieved = data.RetrieveOrder(order);

            order.ProductType = "Wooood";

            Assert.IsTrue(data.UpdateOrder(order));
        }

        [Test]
        public void CanDeleteProductionOrder()
        {
            IOrderRepository data = new ProductionOrdersRepository(@"C:\Data\LiveData");
            DateTime date = DateTime.Parse("01/01/1111");
            List<Order> orders = data.RetrieveOrdersByDate(date);

            int id = orders.Select(o => o.OrderNumber).Max();
            Order order = new Order();
            order.OrderDate = DateTime.Parse("01/01/1111");
            order.OrderNumber = id;

            Assert.AreEqual(true, data.DeleteOrder(order));
        }
    }
}
