using System;
using FOS.BLL;
using FOS.Models;
using FOS.UI.ConsoleIO;
using FOS.Models.Responses;

namespace FOS.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display Orders");
            Console.WriteLine("------------------------");

            DateTime date = Input.GetOrderDateInput();

            Response response = manager.RetrieveOrdersByDate(date);

            if (response.Success)
                Output.DisplayOrderList(response.OrderList);
            else
                Console.WriteLine("\nAn error occured " + response.Message);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
