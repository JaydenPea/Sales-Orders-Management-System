using SalesOrders.Shared.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.Shared.Orders
{
    public interface IOrderService
    {
        Task<List<viewOrdersVM>> GetOrders(viewOrdersFilters filters);
        Task<ServiceResponse<List<viewOrdersVM>>> UpdateOrder(viewOrdersVM order);
        Task<ServiceResponse<List<viewOrdersVM>>> AddOrder(viewOrdersVM order);
        Task<ServiceResponse<List<viewOrdersVM>>> Delete(long id);
    }
}
