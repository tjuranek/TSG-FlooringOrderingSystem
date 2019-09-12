using System.Collections.Generic;

namespace FOS.Models.Responses
{
    public class Response
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public Order Order { get; set; }

        public List<Order> OrderList { get; set; }
    }
}
