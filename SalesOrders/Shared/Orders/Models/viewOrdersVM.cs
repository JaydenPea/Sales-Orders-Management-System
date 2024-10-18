using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders.Models
{
    public class viewOrdersVM
    {
        public long orderId { get; set; }
        public string orderNumber { get; set; } = string.Empty;
        public string orderType { get; set; } = string.Empty;
        public string orderStatus { get; set; } = string.Empty;
        public string customerName { get; set; } = string.Empty;
        public DateTime createdDate { get; set; }
        public List<OrderLineVM> orderLines { get; set; } = new List<OrderLineVM>();

        [NotMapped]
        public bool isEditing { get; set; } =false;
        [NotMapped]
        public bool isNew { get; set; } = false;
        [NotMapped]
        public bool showOrderLines { get; set; } = false;

    }

    //public class OrderHeaderVM
    //{
    //    public long orderId { get; set; }
    //    public string orderNumber { get; set; } = string.Empty;
    //    public string orderType { get; set; } = string.Empty;
    //    public string orderStatus { get; set; } = string.Empty;
    //    public string customerName { get; set; } = string.Empty;
    //    public DateTime createdDate { get; set; }
    //    public List<OrderLineVM> orderLines { get; set; } = new List<OrderLineVM>();
    //}

    public class OrderLineVM
    {
        public long lineNumber { get; set; }
        public string productCode { get; set; } = string.Empty;
        public string productType { get; set; } = string.Empty;
        public decimal costPrice { get; set; } = 0;
        public decimal salesPrice { get; set; } = 0;
        public long quantity { get; set; }

        [NotMapped]
        public bool isEditing { get; set; } = false;
        [NotMapped]
        public bool isNew { get; set; } = false;
    }
}
