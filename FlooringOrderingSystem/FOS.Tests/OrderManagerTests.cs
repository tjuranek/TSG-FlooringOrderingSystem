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

        [TestCase("02/02/2222", "Thomas", "MN", "Wood", false)] //wrong state
        [TestCase("01/01/1111", "Thomas", "OH", "Wood", false)] //wrong date
        [TestCase("02/02/2222", "*4@?", "OH", "Wood", false)] //wrong name
        [TestCase("02/02/2222", "Thomas", "MN", "Water", false)] //wrong material
        [TestCase("02/02/2222", "Thomas", "OH", "Wood", true)] //correct
        public void CreateOrder(string date, string name, string state, string product, bool expected)
        {
            Order order = new Order();
            order.OrderDate = DateTime.Parse(date);
            order.CustomerName = name;
            order.State = state;
            order.ProductType = product;

            bool isValid = true;

            if (order.OrderDate < DateTime.Now)
                isValid = false;

            string validChars = "abcdefghijklmnopqrstuvwxyz123456789,. ";
            if (order.CustomerName.Any(x => !validChars.Contains(x.ToString().ToLower())))
                isValid = false;

            ITaxRepository taxrepo = new ProductionTaxesRepository(@"C:\Data\LiveData\Taxes.txt");
            var Taxes = taxrepo.RetrieveTaxes();

            if (!Taxes.Any(t => t.StateAbbreviation.ToLower() == order.State.ToLower()))
                isValid = false;

            IProductRepository productrepo = new ProductionProductsRepository(@"C:\Data\LiveData\Products.txt");
            var Products = productrepo.RetrieveProducts();

            if (!Products.Any(p => p.ProductType.ToLower() == order.ProductType.ToLower()))
                isValid = false;

            bool result = false;

            if (isValid)
                result = manager.CreateOrder(order).Success;

            Assert.AreEqual(expected, result);
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

        [TestCase("02/02/2222", "Thomas", "MN", "Wood", false)] //wrong state
        [TestCase("01/01/1111", "Thomas", "OH", "Wood", false)] //wrong date
        [TestCase("02/02/2222", "*4@?", "OH", "Wood", false)] //wrong name
        [TestCase("02/02/2222", "Thomas", "MN", "Water", false)] //wrong material
        [TestCase("02/02/2222", "Thomas", "OH", "Wood", true)] //correct
        public void UpdateOrder(string date, string name, string state, string product, bool expected)
        {
            Order order = new Order();
            order.OrderDate = DateTime.Parse(date);
            order.CustomerName = name;
            order.State = state;
            order.ProductType = product;

            bool isValid = true;

            if (order.OrderDate < DateTime.Now)
                isValid = false;

            string validChars = "abcdefghijklmnopqrstuvwxyz123456789,. ";
            if (order.CustomerName.Any(x => !validChars.Contains(x.ToString().ToLower())))
                isValid = false;

            ITaxRepository taxrepo = new ProductionTaxesRepository(@"C:\Data\LiveData\Taxes.txt");
            var Taxes = taxrepo.RetrieveTaxes();

            if (!Taxes.Any(t => t.StateAbbreviation.ToLower() == order.State.ToLower()))
                isValid = false;

            IProductRepository productrepo = new ProductionProductsRepository(@"C:\Data\LiveData\Products.txt");
            var Products = productrepo.RetrieveProducts();

            if (!Products.Any(p => p.ProductType.ToLower() == order.ProductType.ToLower()))
                isValid = false;

            if (isValid)
                manager.CreateOrder(order);

            Order orderTwo = order;
            orderTwo.CustomerName = "Thomas";

            bool result = manager.UpdateOrder(orderTwo).Success;

            Assert.AreEqual(expected, result);
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
