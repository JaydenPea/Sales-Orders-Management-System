using SalesOrders.Shared.Orders.Models;

namespace SalesOrders.Client.Service.OrdersService
{
    public interface IOrderService
    {
        event Action OnChange;
        List<viewOrdersVM> viewOrdersVMs { get; set; }
        Task GetOrders(viewOrdersFilters filters);
    }
}
