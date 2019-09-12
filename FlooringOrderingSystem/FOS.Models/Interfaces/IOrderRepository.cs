using System;
using System.Collections.Generic;

namespace FOS.Models.Interfaces
{
    public interface IOrderRepository
    {
        bool CreateOrder(Order order);

        Order RetrieveOrder(Order order);

        List<Order> RetrieveOrdersByDate(DateTime date);

        bool UpdateOrder(Order order);

        bool DeleteOrder(Order order);
    }
}
