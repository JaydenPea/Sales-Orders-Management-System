using Microsoft.Extensions.Configuration;
using SalesOrders.DAL.Models;
using SalesOrders.Shared.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders
{
    public class OrderService : IOrderService
    {
        #region Injections
        private readonly SalesOrderDBContext _context;
        #endregion

        #region Constructors
        public OrderService(SalesOrderDBContext context)
        {
            _context = context;
        }

        #endregion

        #region Service Methods

        public async Task<List<viewOrdersVM>> GetOrders(viewOrdersFilters filters)
        {
            var orders = from OH in _context.OrderHeader
                         select new viewOrdersVM()
                         {
                             orderId = OH.orderId,
                             orderNumber = OH.orderNumber,
                             orderType = OH.orderType,
                             customerName = OH.customerName,
                             createdDate = OH.createdDate,
                             orderStatus = OH.orderStatus,

                             //order lines 
                             orderLines = (from OL in _context.OrderLine
                                           where OL.orderId == OH.orderId
                                           select new OrderLineVM()
                                           {
                                               lineNumber = OL.lineNumber,
                                               productCode = OL.productCode,
                                               productType = OL.productType,
                                               costPrice = OL.costPrice,
                                               salesPrice = OL.salesPrice,
                                               quantity = OL.quantity,
                                           }).ToList()
                         };

            //Dont bring into memory just yet. Lets apply filtering server side.
            #region Filters

            if(!string.IsNullOrEmpty( filters.orderType))
            {
                orders = orders.Where(x => x.orderType == filters.orderType);
            }

            if (filters.from != DateTime.MinValue)
            {
                if (filters.to != DateTime.MinValue)
                {
                    orders = orders.Where(x => x.createdDate >= filters.from && x.createdDate <= filters.to);
                }
                else
                {
                    orders = orders.Where(x => x.createdDate >= filters.from);
                }
            }

            #endregion

            var result = orders.ToList();
            return result;
        }
        #endregion
    }
}
