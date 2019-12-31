using System;
using System.Collections.Generic;
using System.Linq;
using FOS.Models;
using FOS.Models.Responses;
using FOS.Models.Interfaces;

namespace FOS.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orders;
        private IProductRepository _products;
        private ITaxRepository _taxes;

        public List<Product> Products = new List<Product>();
        public List<Tax> Taxes = new List<Tax>();

        public OrderManager(IOrderRepository orders, IProductRepository products, ITaxRepository taxes)
        {
            _orders = orders;
            _products = products;
            _taxes = taxes;

            Products = _products.RetrieveProducts();
            Taxes = _taxes.RetrieveTaxes();
        }

        #region CRUD methods
        public Response CreateOrder(Order order)
        {
            Response response = new Response();

            bool isValid = true;

            if (order.OrderDate < DateTime.Now)
                isValid = false;

            string validChars = "abcdefghijklmnopqrstuvwxyz123456789,. ";
            if (order.CustomerName.Any(x => !validChars.Contains(x.ToString().ToLower())))
                isValid = false;

            if (!Taxes.Any(t => t.StateAbbreviation.ToLower() == order.State.ToLower()))
                isValid = false;

            if (!Products.Any(p => p.ProductType.ToLower() == order.ProductType.ToLower()))
                isValid = false;

            if (isValid)
            {
                if (_orders.CreateOrder(order))
                    response.Success = true;
                else
                {
                    response.Success = false;
                    response.Message = "Error: Unable to create order.";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Error: Unable to create order.";
            }

            return response;
        }

        public Response RetrieveOrder(Order order)
        {
            Response response = new Response();
            Order retrieved = _orders.RetrieveOrder(order);

            if (retrieved != null)
            {
                response.Success = true;
                response.Order = retrieved;
            }
            else
            {
                response.Success = false;
                response.Message = "Error: Unable to retrieve order.";
            }

            return response;
        }

        public Response RetrieveOrdersByDate(DateTime date)
        {
            Response response = new Response();
            List<Order> retrieved = _orders.RetrieveOrdersByDate(date);

            if (retrieved != null || retrieved.Count != 0)
            {
                response.Success = true;
                response.OrderList = retrieved;
            }
            else
            {
                response.Success = false;
                response.Message = "Error: Unable to retrieve orders.";
            }

            return response;
        }

        public Response UpdateOrder(Order order)
        {
            Response response = new Response();

            if (_orders.UpdateOrder(order))
                response.Success = true;
            else
            {
                response.Success = false;
                response.Message = "Error: Unable to update order.";
            }

            return response;
        }

        public Response DeleteOrder(Order order)
        {
            Response response = new Response();

            if (_orders.DeleteOrder(order))
                response.Success = true;
            else
            {
                response.Success = false;
                response.Message = "Error: Unable to delete order.";
            }

            return response;
        }
        #endregion

        public Order GenerateOrder(DateTime orderDate, string customerName, string state, string productType, decimal area)
        {
            Order order = new Order();

            order.OrderNumber = 1; //default, data repo will take care of this
            order.CustomerName = customerName;
            order.State = state;
            order.ProductType = productType;
            order.Area = area;
            order.OrderDate = orderDate;

            foreach (Product product in _products.RetrieveProducts())
            {
                if (product.ProductType.ToLower() == order.ProductType.ToLower())
                {
                    order.CostPerSquareFoot = product.CostPerSquareFoot;
                    order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
                }
            } 

            foreach (Tax tax in _taxes.RetrieveTaxes())
            {
                if (tax.StateAbbreviation.ToLower() == order.State.ToLower())
                    order.TaxRate = tax.TaxRate;
            }

            order.MaterialCost = order.Area * order.CostPerSquareFoot;
            order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
            order.Tax = (order.MaterialCost + order.LaborCost) * (order.TaxRate / 100);
            order.Total = order.MaterialCost + order.LaborCost + order.Tax;

            return order;
        }
    }
}
