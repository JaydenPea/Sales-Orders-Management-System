using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders.Models
{
    public class viewOrdersFilters
    {
        public string orderType { get; set; } = string.Empty;
        public DateTime? from {  get; set; } = DateTime.MinValue;
        public DateTime? to { get; set; } = DateTime.MinValue;
        public string productCode { get; set; } = string.Empty;
    }
}
