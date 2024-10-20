using SalesOrders.Shared.Orders.Models;

namespace SalesOrders.Client.Service.OrdersService
{
    public interface IOrderService
    {
        event Action OnChange;
        List<viewOrdersVM> viewOrdersVMs { get; set; }
        Task GetOrders(viewOrdersFilters filters);
        Task AddOrder(viewOrdersVM view);
        Task DeleteOrder(long id);
        Task UpdateOrder(viewOrdersVM view);
        Task UpdateOrderLine(OrderLineVM view);

        //OrderStats
        Task<OrderTypeStatsVM> GetOrderTypeStats();
        Task<OrderHeaderStatsVM> GetHeaderStats();
    }
}
