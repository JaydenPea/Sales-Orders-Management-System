using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResponse<List<viewOrdersVM>>> GetOrders(viewOrdersFilters filters)
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
                                               lineId = OL.lineId,
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
            return new ServiceResponse<List<viewOrdersVM>>
            {
                Data = result
            };
        }

        public async Task<ServiceResponse<List<viewOrdersVM>>> UpdateOrder(viewOrdersVM order)
        {
            var orderCheck = await GetOrderById(order.orderId);
            if(orderCheck == null)
            {
                return new ServiceResponse<List<viewOrdersVM>>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            orderCheck.customerName = order.customerName;
            orderCheck.orderType = order.orderType;
            orderCheck.orderNumber = order.orderNumber;
            orderCheck.orderStatus = order.orderStatus;
            orderCheck.createdDate = order.createdDate;
            await _context.SaveChangesAsync();

            viewOrdersFilters filters = new viewOrdersFilters();
            return await GetOrders(filters);
        }

        public async Task<ServiceResponse<List<viewOrdersVM>>> AddOrder(viewOrdersVM order)
        {
            order.isEditing = order.isNew = false;

            //add header

            OrderHeader orderHeader = new OrderHeader()
            {
                orderNumber = order.orderNumber,
                orderType = order.orderType,
                orderStatus = order.orderStatus,
                customerName = order.customerName,
                createdDate = order.createdDate,
            };

            _context.OrderHeader.Add(orderHeader);
            await _context.SaveChangesAsync();

            long lineNumber = 0;
            foreach(var lineItem in order.orderLines)
            {
                //auto increment line number foreach line item linked to the header.
                lineNumber++;

                //add line items for the header.
                OrderLine orderLine = new OrderLine()
                {
                    orderId = orderHeader.orderId,
                    lineNumber = lineNumber,
                    productCode = lineItem.productCode,
                    productType = lineItem.productType,
                    costPrice = lineItem.costPrice,
                    salesPrice = lineItem.salesPrice,
                    quantity = lineItem.quantity,
                };

                _context.OrderLine.Add(orderLine);
            }

            //save changes to the db
            await _context.SaveChangesAsync();
            viewOrdersFilters filters = new viewOrdersFilters();
            return await GetOrders(filters);
        }

        public async Task<ServiceResponse<List<viewOrdersVM>>> Delete(long id)
        {
            //Delete header item and all line items linked to it. This is going to be a hard delete.
            var orderHeader = await _context.OrderHeader.FirstOrDefaultAsync(o => o.orderId == id);

            // Check if the order exists
            if (orderHeader == null)
            {
                return new ServiceResponse<List<viewOrdersVM>>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            // Retrieve all the line items linked to this order header
            var orderLines = await _context.OrderLine.Where(ol => ol.orderId == id).ToListAsync();

            // Remove all the order lines associated with the order header
            if (orderLines.Any())
            {
                _context.OrderLine.RemoveRange(orderLines);
            }

            // Remove the order header itself
            _context.OrderHeader.Remove(orderHeader);
            await _context.SaveChangesAsync();

            viewOrdersFilters filters = new viewOrdersFilters();
            return await GetOrders(filters);



        }

        public async Task<ServiceResponse<List<viewOrdersVM>>> UpdateLineOrders(OrderLineVM order)
        {

                // Retrieve the existing line order by lineId
                var lineOrder = await GetOrderLineById(order.lineId);
                if (lineOrder == null)
                {
                    return new ServiceResponse<List<viewOrdersVM>>
                    {
                        Success = false,
                        Message = $"Order line with ID {order.lineId} not found."
                    };
                }

                // Update the properties of the line
                lineOrder.productCode = order.productCode;
                lineOrder.productType = order.productType;
                lineOrder.costPrice = order.costPrice;
                lineOrder.salesPrice = order.salesPrice;
                lineOrder.quantity = order.quantity;

                _context.OrderLine.Update(lineOrder);
            

            // Save the changes
            await _context.SaveChangesAsync();
            viewOrdersFilters filters = new viewOrdersFilters();
            return await GetOrders(filters);
        }
        #endregion

        #region Private helpers
        private async Task<OrderHeader> GetOrderById(long id)
        {
            return await _context.OrderHeader.FirstOrDefaultAsync(c => c.orderId == id);
        }

        private async Task<OrderLine> GetOrderLineById(long id)
        {
            return await _context.OrderLine.FirstOrDefaultAsync(c => c.lineId == id);
        }
        #endregion
    }
}
