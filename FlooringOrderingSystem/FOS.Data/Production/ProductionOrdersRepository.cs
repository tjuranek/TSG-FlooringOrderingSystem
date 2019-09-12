using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Production
{
    public class ProductionOrdersRepository : IOrderRepository
    {
        private string _basePath = "";

        public ProductionOrdersRepository(string basePath)
        {
            _basePath = basePath;
        }

        #region CRUD Methods
        public bool CreateOrder(Order order)
        {
            string path = DateToFilePathConverter(order.OrderDate);

            if (File.Exists(path))
                order.OrderNumber = GetNextAvailableOrderNumber(RetrieveOrdersByDate(order.OrderDate));
            else
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("OrderNumber|OrderDate|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total");
                }

                order.OrderNumber = 1;
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(order.OrderNumber + "|" +
                    order.OrderDate + "|" +
                    order.CustomerName + "|" +
                    order.State + "|" +
                    order.TaxRate + "|" +
                    order.ProductType + "|" +
                    order.Area + "|" +
                    order.CostPerSquareFoot + "|" +
                    order.LaborCostPerSquareFoot + "|" +
                    order.MaterialCost + "|" +
                    order.LaborCost + "|" +
                    order.Tax + "|" +
                    order.Total);
            }

            return true;
        }

        public bool DeleteOrder(Order order)
        {
            List<Order> orders = RetrieveOrdersByDate(order.OrderDate);

            order = orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);

            if (order == null)
                return false;

            orders.Remove(order);

            WriteOrderListToFile(order.OrderDate, orders);

            return true;
        }

        public Order RetrieveOrder(Order order)
        {
            List<Order> orders = RetrieveOrdersByDate(order.OrderDate);

            return orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
        }

        public List<Order> RetrieveOrdersByDate(DateTime date)
        {
            string path = DateToFilePathConverter(date);
            List<Order> orders = new List<Order>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    sr.ReadLine(); // this skips the header line in the file

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order order = new Order();
                        string[] columns = line.Split('|');

                        order.OrderNumber = int.Parse(columns[0]);
                        order.OrderDate = DateTime.Parse(columns[1]);
                        order.CustomerName = columns[2];
                        order.State = columns[3];
                        order.TaxRate = decimal.Parse(columns[4]);
                        order.ProductType = columns[5];
                        order.Area = decimal.Parse(columns[6]);
                        order.CostPerSquareFoot = decimal.Parse(columns[7]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[8]);
                        order.MaterialCost = decimal.Parse(columns[9]);
                        order.LaborCost = decimal.Parse(columns[10]);
                        order.Tax = decimal.Parse(columns[11]);
                        order.Total = decimal.Parse(columns[12]);

                        orders.Add(order);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred reading the file: {ex.Message}");
            }

            if (orders.Count > 0)
                return orders;

            return null;
        }

        public bool UpdateOrder(Order order)
        {
            List<Order> orders = RetrieveOrdersByDate(order.OrderDate);

            Order oldOrder = orders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);

            if (oldOrder == null)
                return false;

            order.OrderNumber = oldOrder.OrderNumber;

            orders.Remove(oldOrder);
            orders.Add(order);

            WriteOrderListToFile(order.OrderDate, orders);

            return true;
        }
        #endregion

        private string DateToFilePathConverter(DateTime date)
        {
            return _basePath + @"\Orders\Orders_" + date.ToString("MMddyyyy") + ".txt";
        }

        private int GetNextAvailableOrderNumber(List<Order> orders)
        {
            return orders.Select(o => o.OrderNumber).Max() + 1;
        }

        private void WriteOrderListToFile(DateTime date, List<Order> orders)
        {
            string path = DateToFilePathConverter(date);

            if (File.Exists(path))
                File.Delete(path);

            orders = orders.OrderBy(o => o.OrderNumber).ToList();

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("OrderNumber|OrderDate|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total");

                foreach (Order order in orders)
                {
                    sw.WriteLine(order.OrderNumber + "|" +
                        order.OrderDate + "|" +
                        order.CustomerName + "|" +
                        order.State + "|" +
                        order.TaxRate + "|" +
                        order.ProductType + "|" +
                        order.Area + "|" +
                        order.CostPerSquareFoot + "|" +
                        order.LaborCostPerSquareFoot + "|" +
                        order.MaterialCost + "|" +
                        order.LaborCost + "|" +
                        order.Tax + "|" +
                        order.Total
                    );
                }
            }
        }
    }
}
