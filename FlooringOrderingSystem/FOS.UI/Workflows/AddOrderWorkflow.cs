using System;
using FOS.BLL;
using FOS.Models;
using FOS.UI.ConsoleIO;

namespace FOS.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Add Order");
            Console.WriteLine("------------------------");

            DateTime orderDate = Input.GetOrderDateInput();
            string customerName = Input.GetCustomerNameInput();
            string state = Input.GetStateInput();
            string productType = Input.GetProductInput();
            decimal area = Input.GetAreaInput();

            Order order = manager.GenerateOrder(orderDate, customerName, state, productType, area);

            Output.DisplayOrder(order);

            if (Input.ConfirmSubmisison())
                manager.CreateOrder(order);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
