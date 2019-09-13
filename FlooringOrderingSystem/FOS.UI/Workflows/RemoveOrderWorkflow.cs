using System;
using FOS.BLL;
using FOS.Models;
using FOS.UI.ConsoleIO;
using FOS.Models.Responses;

namespace FOS.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Remove Order");
            Console.WriteLine("------------------------");

            DateTime orderDate = Input.GetOrderDateInput();
            int orderNumber = Input.InputOrderNumber();

            Order orderToDelete = new Order();
            orderToDelete.OrderNumber = orderNumber;
            orderToDelete.OrderDate = orderDate;

            Output.DisplayOrder(manager.RetrieveOrder(orderToDelete).Order);

            if (Input.ConfirmSubmisison())
                manager.DeleteOrder(orderToDelete);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
