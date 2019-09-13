using System;
using FOS.BLL;
using FOS.Models;
using FOS.UI.ConsoleIO;
using FOS.Models.Responses;

namespace FOS.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Edit Order");
            Console.WriteLine("------------------------");

            DateTime orderDate = Input.GetOrderDateInput();
            int orderNumber = Input.InputOrderNumber();

            Order order = manager.RetrieveOrder(new Order
            {
                OrderDate = orderDate,
                OrderNumber = orderNumber
            }).Order;

            if (order != null)
            {
                order.CustomerName = Input.GetCustomerNameInput(order.CustomerName);
                order.State = Input.GetStateInput(order.State);
                order.ProductType = Input.GetProductInput(order.ProductType);
                order.Area = Input.GetAreaInput(order.Area);
            }

            order = manager.GenerateOrder(orderDate, order.CustomerName, order.State, order.ProductType, order.Area);

            Output.DisplayOrder(order);

            if (Input.ConfirmSubmisison())
                manager.UpdateOrder(order);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
