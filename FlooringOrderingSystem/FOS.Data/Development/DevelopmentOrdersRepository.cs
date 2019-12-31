using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Development
{
    public class DevelopmentOrdersRepository : IOrderRepository
    {
        private List<Order> _orders = new List<Order>();

        public DevelopmentOrdersRepository()
        {
            _orders.Add(new Order(1, "Held Construction One", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m));
            _orders.Add(new Order(2, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m));
            _orders.Add(new Order(3, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m));
            _orders.Add(new Order(4, "Held Construction", "IN", 6.00m, "Wood", 100.00m, 5.15m, 4.75m, 515.00m, 475.00m, 61.88m, 1051.88m));

            Order o = new Order();
            o.OrderDate = DateTime.Parse("01/01/1111");

            _orders.Add(o);
        }

        public bool CreateOrder(Order order)
        {
            _orders.Add(order);

            return true;
        }

        public bool DeleteOrder(Order order)
        {
            _orders.Remove(order);

            return true;
        }

        public Order RetrieveOrder(Order order)
        {
            List<Order> orders = RetrieveOrdersByDate(order.OrderDate);

            return orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
        }

        public List<Order> RetrieveOrdersByDate(DateTime date)
        {
            return _orders.Where(o => o.OrderDate == date).ToList();
        }

        public bool UpdateOrder(Order order)
        {
            Order retrieved = _orders.FirstOrDefault(o => o.OrderDate == order.OrderDate && o.OrderNumber == order.OrderNumber);

            _orders.Remove(retrieved);
            _orders.Add(order);

            return true;
        }
    }
}
