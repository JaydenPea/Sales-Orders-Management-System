using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders.Models
{
    public class OrderHeaderStatsVM
    {
        public List<Dictionary<string, DateTime>> orderHeaders { get; set; } = new List<Dictionary<string, DateTime>>();
        public long orderCount { get; set; } = 0;
        public long customerCount { get; set; } = 0;
    }
}
