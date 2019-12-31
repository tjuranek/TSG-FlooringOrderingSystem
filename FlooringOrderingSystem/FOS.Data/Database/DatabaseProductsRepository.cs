using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Database
{
    public class DatabaseProductsRepository : IProductRepository
    {
        public List<Product> RetrieveProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source=DESKTOP-VN0TUCD; Initial Catalog=Flooring; Integrated Security=True; Trusted_Connection=true";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT ProductType, CostPerSquareFoot, LaborCostPerSquareFoot FROM Product;", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductType = reader[0].ToString(),
                            CostPerSquareFoot = decimal.Parse(reader[1].ToString()),
                            LaborCostPerSquareFoot = decimal.Parse(reader[2].ToString())
                        });
                    }
                }
            }

            return products;
        }
    }
}
