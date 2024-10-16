using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders.Models
{
    public class OrderResponse<T>
    {
        public T? data {  get; set; }
        public bool success { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
