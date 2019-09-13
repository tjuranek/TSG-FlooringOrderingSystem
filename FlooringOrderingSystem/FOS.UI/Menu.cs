using System;
using FOS.UI.Workflows;

namespace FOS.UI
{
    public class Menu
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("*********************************************************");
                Console.WriteLine("* Flooring Program");
                Console.WriteLine("*");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add an Order");
                Console.WriteLine("3. Edit an Order");
                Console.WriteLine("4. Remove an Order");

                Console.WriteLine("\nQ to quit");
                Console.WriteLine("*********************************************************");

                Console.Write("\nEnter selection: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayOrdersWorkflow displayOrders = new DisplayOrdersWorkflow();
                        displayOrders.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addOrder = new AddOrderWorkflow();
                        addOrder.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrder = new EditOrderWorkflow();
                        editOrder.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrder = new RemoveOrderWorkflow();
                        removeOrder.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}
