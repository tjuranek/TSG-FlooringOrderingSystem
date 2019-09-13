using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FOS.Models.Interfaces;
using FOS.BLL;
using FOS.Data.Production;
using FOS.Models;


namespace FOS.Tests
{
    [TestFixture]
    public class OrderManagerTests
    {
        private OrderManager manager = new OrderManager(new ProductionOrdersRepository(@"C:\Data\LiveData"), new ProductionProductsRepository(@"C:\Data\LiveData\Products.txt"), new ProductionTaxesRepository(@"C:\Data\LiveData\Taxes.txt"));

        [Test]
        public void CreateOrder()
        {
            Order order = new Order(1, "Held Construction One", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m);
            order.OrderDate = DateTime.Parse("05/05/5555");

            Assert.AreEqual(true, manager.CreateOrder(order).Success);
        }

        [Test]
        public void RetrieveOrder()
        {
            Order order = new Order();
            order.OrderDate = DateTime.Parse("05/05/5555");
            order.OrderNumber = 1;

            Order retrieved = manager.RetrieveOrder(order).Order;

            Assert.IsTrue(retrieved.CustomerName == "Held Construction One");
        }

        [Test]
        public void RetrieveOrdersByDate()
        {
            DateTime date = DateTime.Parse("05/05/5555");

            Assert.IsTrue(manager.RetrieveOrdersByDate(date).OrderList.Count > 0);
        }

        [Test]
        public void UpdateOrder()
        {
            Order order = new Order(2, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m);
            order.OrderDate = DateTime.Parse("06/06/6666");

            manager.CreateOrder(order);

            Order orderTwo = order;
            orderTwo.CustomerName = "Thomas";

            manager.UpdateOrder(orderTwo);

            Assert.AreEqual("Thomas", manager.RetrieveOrder(orderTwo).Order.CustomerName);
        }

        [Test]
        public void DeleteOrder()
        {
            Order order = new Order(2, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m);
            order.OrderDate = DateTime.Parse("07/07/7777");

            int num = manager.RetrieveOrdersByDate(DateTime.Parse("07/07/7777")).OrderList.Count + 1;

            manager.CreateOrder(order);
            manager.CreateOrder(order);

            manager.DeleteOrder(order);

            Assert.IsTrue(manager.RetrieveOrdersByDate(DateTime.Parse("07/07/7777")).OrderList.Count == num);
        }
    }
}
