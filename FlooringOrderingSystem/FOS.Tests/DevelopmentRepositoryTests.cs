using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FOS.Data.Development;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Tests
{
    [TestFixture]
    public class DevelopmentRepositoryTests
    {
        [Test]
        public void CanRetrieveDevelopmentTaxes()
        {
            ITaxRepository data = new DevelopmentTaxesRepository();

            List<Tax> taxes = data.RetrieveTaxes();

            Assert.IsTrue(taxes.Count > 0);
        }

        [Test]
        public void CanRetrieveDevelopmentProducts()
        {
            IProductRepository data = new DevelopmentProductsRepository();

            List<Product> products = data.RetrieveProducts();

            Assert.IsTrue(products.Count > 0);
        }

        [Test]
        public void CanCreateDevelopmentProduct()
        {
            IOrderRepository data = new DevelopmentOrdersRepository();

            Order order = new Order();
            order.OrderDate = DateTime.Parse("01/01/1111");
            order.CustomerName = "Thomas";

            Assert.IsTrue(data.CreateOrder(order));
        }

        [Test]
        public void CanRetrieveDevelopmentOrders()
        {
            IOrderRepository data = new DevelopmentOrdersRepository();

            Order search = new Order();
            search.OrderNumber = 1;

            Order retrieved = data.RetrieveOrder(search);

            Assert.IsTrue(retrieved.OrderNumber == 1 && retrieved.CustomerName == "Held Construction One");
        }

        [Test]
        public void CanRetrieveDevelopmentOrdersByDate()
        {
            IOrderRepository data = new DevelopmentOrdersRepository();

            DateTime date = DateTime.Parse("01/01/1111");

            List<Order> orders = data.RetrieveOrdersByDate(date);

            Assert.IsTrue(orders.Count > 0);
        }

        [Test]
        public void CanUpdateDevelopmentOrder()
        {
            IOrderRepository data = new DevelopmentOrdersRepository();

            Order order = new Order(1, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m);

            Assert.IsTrue(data.UpdateOrder(order));    
        }

        [Test]
        public void CanDeleteDevelopmentOrder()
        {
            IOrderRepository data = new DevelopmentOrdersRepository();

            Order order = new Order(1, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m);

            Assert.IsTrue(data.DeleteOrder(order));
        }
    }
}
