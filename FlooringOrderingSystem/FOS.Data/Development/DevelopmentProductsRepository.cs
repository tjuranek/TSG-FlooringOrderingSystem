using System.Collections.Generic;
using FOS.Models;
using FOS.Models.Interfaces;

namespace FOS.Data.Development
{
    public class DevelopmentProductsRepository : IProductRepository
    {
        public List<Product> RetrieveProducts()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product
            {
                ProductType = "Carpet",
                CostPerSquareFoot = 2.25m,
                LaborCostPerSquareFoot = 2.10m
            });

            products.Add(new Product
            {
                ProductType = "Laminate",
                CostPerSquareFoot = 1.75m,
                LaborCostPerSquareFoot = 2.10m
            });

            products.Add(new Product
            {
                ProductType = "Tile",
                CostPerSquareFoot = 3.50m,
                LaborCostPerSquareFoot = 4.15m
            });

            products.Add(new Product
            {
                ProductType = "Wood",
                CostPerSquareFoot = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            });

            return products;
        }
    }
}
