using System;
using System.Linq;
using FOS.BLL;

namespace FOS.UI.ConsoleIO
{
    public class Input
    {
        public static int InputOrderNumber()
        {
            while (true)
            {
                Console.Write("Enter Order Number: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number) && number > 0)
                    return number;
                else
                    Console.WriteLine("ERROR: Enter a valid order number.");
            }
        }

        public static DateTime GetOrderDateInput()
        {
            while (true)
            {
                Console.Write("Enter a order date (mm/dd/yyyy): ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;
                else
                    Console.WriteLine("ERROR: Enter a date with a valid month/day/year format.");
            }
        }

        public static bool ConfirmSubmisison()
        {
            Console.Write("Would you like to submit (y/n): ");

            if (Console.ReadLine().ToUpper() == "Y")
                return true;

            return false;
        }

        // the following have optional paramaters to support both add and edit order methods
        public static string GetCustomerNameInput(string name = "")
        {
            while (true)
            {
                Console.Write("[" + name + "] Enter a customer name: ");

                string validChars = "abcdefghijklmnopqrstuvwxyz123456789,. ";
                string input = Console.ReadLine();

                if (input == null || input == "")
                    return name;

                if (input.Any(x => !validChars.Contains(x.ToString().ToLower())))
                    Console.WriteLine("ERROR: Enter a customer name with valid characters ([a-z] [1-9] , .) :");
                else
                    return input;
            }
        }

        public static string GetStateInput(string state = "")
        {
            while (true)
            {
                OrderManager manager = OrderManagerFactory.Create();

                Console.Write("[" + state + "] Enter a state (ex. MN): ");

                string input = Console.ReadLine();

                if (state.ToLower() == input.ToLower() || input == null || input == "")
                    return state;

                if (manager.Taxes.Any(x => x.StateAbbreviation == input.ToUpper()))
                    return input;
                else
                    Console.WriteLine("ERROR: Enter a valid state abbreviation.");
            }
        }

        public static string GetProductInput(string productType = "")
        {
            while (true)
            {
                OrderManager manager = OrderManagerFactory.Create();

                Console.Write("[" + productType + "] Please enter a Product Type: ");

                string input = Console.ReadLine();

                if (input.ToLower() == productType.ToLower() || input == null || input == "")
                    return productType;

                if (manager.Products.Any(x => x.ProductType.ToLower() == input.ToLower()))
                    return input;
                else
                    Console.WriteLine("ERROR: Enter a valid product type.");
            }
        }

        public static decimal GetAreaInput(decimal areaInput = 0.00m)
        {
            while (true)
            {
                Console.Write("[" + areaInput + "] Please enter an Area: ");

                string input = Console.ReadLine();

                if (input == null || input == "")
                    return areaInput;

                if (decimal.TryParse(input, out decimal area) && area > 100)
                    return area;
                else
                    Console.WriteLine("ERROR: Enter a valid area over 100 sq ft.");
            }
        }
    }
}
