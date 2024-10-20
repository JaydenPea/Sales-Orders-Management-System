using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders.Models
{
    public class OrderTypeStatsVM
    {
        public List<Dictionary<string, long>> orderTypes {  get; set; } = new List<Dictionary<string, long>>(); 
    }
}
