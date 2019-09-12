using System;

namespace FOS.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string State { get; set; }
        public decimal TaxRate { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public Order()
        {

        }

        public Order(int orderNumber, string customerName, string state, decimal taxRate,
                string productType, decimal area, decimal costPerSquareFoot, decimal laborCostPerSquareFoot,
                decimal materialCost, decimal laborCost, decimal tax, decimal total)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            State = state;
            TaxRate = taxRate;
            ProductType = productType;
            Area = area;
            CostPerSquareFoot = costPerSquareFoot;
            LaborCostPerSquareFoot = LaborCostPerSquareFoot;
            MaterialCost = materialCost;
            LaborCost = laborCost;
            Tax = tax;
            Total = total;
        }
    }
}
