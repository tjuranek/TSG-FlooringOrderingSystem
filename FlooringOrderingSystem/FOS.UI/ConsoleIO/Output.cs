using System;
using System.Collections.Generic;
using System.Linq;
using FOS.Models;
using FOS.BLL;

namespace FOS.UI.ConsoleIO
{
    public class Output
    {
        public static void DisplayOrderList(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                DisplayOrder(order);
            }
        }

        public static void DisplayOrder(Order order)
        {
            Console.WriteLine();
            Console.WriteLine("*****************************************************************");
            Console.WriteLine(order.OrderNumber + " | " + order.OrderDate.ToString("MM/dd/yyyy"));
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine("Product: " + order.ProductType);
            Console.WriteLine("Materials: " + order.MaterialCost);
            Console.WriteLine("Labor: " + order.LaborCost);
            Console.WriteLine("Tax: " + order.Tax);
            Console.WriteLine("Total: " + order.Total);
            Console.WriteLine("*****************************************************************");
            Console.WriteLine();
        }
    }
}
