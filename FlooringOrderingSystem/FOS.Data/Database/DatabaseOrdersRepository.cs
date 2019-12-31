using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Database
{
    public class DatabaseOrdersRepository : IOrderRepository
    {
        public bool CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order RetrieveOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=DESKTOP-VN0TUCD; Initial Catalog=Flooring; Integrated Security=True; Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT OrderID, OrderDate, Customer.CustomerName, State.StateAbbreviation,Tax.TaxRate, Product.ProductType, Area, Product.CostPerSquareFoot, Product.LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total FROM Orders INNER JOIN Customer ON Customer.CustomerID = Orders.CustomerID INNER JOIN StateTax ON StateTax.StateTaxID = Orders.StateTaxID INNER JOIN State ON State.StateID = StateTax.StateID INNER JOIN Tax ON Tax.TaxID = StateTax.TaxID INNER JOIN Product on Product.ProductID = Orders.ProductID WHERE OrderID = " + order.OrderNumber + ";", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    order.OrderDate = DateTime.Parse(reader[1].ToString());
                    order.CustomerName = reader[2].ToString();
                    order.State = reader[3].ToString();
                    order.TaxRate = decimal.Parse(reader[4].ToString());
                    order.ProductType = reader[5].ToString();
                    order.Area = decimal.Parse(reader[6].ToString());
                    order.CostPerSquareFoot = decimal.Parse(reader[7].ToString());
                    order.LaborCostPerSquareFoot = decimal.Parse(reader[8].ToString());
                    order.MaterialCost = decimal.Parse(reader[9].ToString());
                    order.LaborCost = decimal.Parse(reader[10].ToString());
                    order.Tax = decimal.Parse(reader[11].ToString());
                    order.Total = decimal.Parse(reader[12].ToString());
                }
            }

            return order;
        }

        public List<Order> RetrieveOrdersByDate(DateTime date)
        {
            List<Order> orders = new List<Order>();
            string datestr = date.ToString("yyyy-MM-dd");

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=DESKTOP-VN0TUCD; Initial Catalog=Flooring; Integrated Security=True; Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT OrderID, OrderDate, Customer.CustomerName, State.StateAbbreviation,Tax.TaxRate, Product.ProductType, Area, Product.CostPerSquareFoot, Product.LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total FROM Orders INNER JOIN Customer ON Customer.CustomerID = Orders.CustomerID INNER JOIN StateTax ON StateTax.StateTaxID = Orders.StateTaxID INNER JOIN State ON State.StateID = StateTax.StateID INNER JOIN Tax ON Tax.TaxID = StateTax.TaxID INNER JOIN Product on Product.ProductID = Orders.ProductID WHERE OrderDate = '" + datestr + "';", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order();

                        order.OrderNumber = int.Parse(reader[0].ToString());
                        order.OrderDate = DateTime.Parse(reader[1].ToString());
                        order.CustomerName = reader[2].ToString();
                        order.State = reader[3].ToString();
                        order.TaxRate = decimal.Parse(reader[4].ToString());
                        order.ProductType = reader[5].ToString();
                        order.Area = decimal.Parse(reader[6].ToString());
                        order.CostPerSquareFoot = decimal.Parse(reader[7].ToString());
                        order.LaborCostPerSquareFoot = decimal.Parse(reader[8].ToString());
                        order.MaterialCost = decimal.Parse(reader[9].ToString());
                        order.LaborCost = decimal.Parse(reader[10].ToString());
                        order.Tax = decimal.Parse(reader[11].ToString());
                        order.Total = decimal.Parse(reader[12].ToString());

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public bool UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
