using System;
using System.Collections.Generic;
using System.IO;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Production
{
    public class ProductionProductsRepository : IProductRepository
    {
        private string _path = "";

        public ProductionProductsRepository(string path)
        {
            _path = path;
        }

        public List<Product> RetrieveProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (StreamReader sr = new StreamReader(_path))
                {
                    sr.ReadLine(); //this skips the header line in the file

                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] columns = line.Split('|');

                        Product product = new Product();
                        product.ProductType = columns[0];
                        product.CostPerSquareFoot = decimal.Parse(columns[1]);
                        product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                        products.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred reading the file: {ex.Message}");
            }

            if (products.Count > 0)
                return products;

            return null;
        }
    }
}
