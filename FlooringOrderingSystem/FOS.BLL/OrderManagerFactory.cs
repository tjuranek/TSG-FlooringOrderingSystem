using System;
using FOS.Data.Development;
using FOS.Data.Production;
using System.Configuration;

namespace FOS.BLL
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Dev":
                    return new OrderManager(new DevelopmentOrdersRepository(), new DevelopmentProductsRepository(), new DevelopmentTaxesRepository());
                case "Prod":
                    return new OrderManager(new ProductionOrdersRepository(@"C:\Data\LiveData"), new ProductionProductsRepository(@"C:\Data\LiveData\Products.txt"), new ProductionTaxesRepository(@"C:\Data\LiveData\Taxes.txt"));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
