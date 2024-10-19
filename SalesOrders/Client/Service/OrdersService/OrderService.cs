
using SalesOrders.Shared;
using SalesOrders.Shared.Orders.Models;
using System.Net.Http.Json;
using System.Text;

namespace SalesOrders.Client.Service.OrdersService
{
    public class OrderService : IOrderService
    {
        #region Injections and Constructors
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public OrderService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }
        #endregion


        public event Action OnChange;
        public List<viewOrdersVM> viewOrdersVMs { get; set; } = new List<viewOrdersVM>();
        public async Task GetOrders(viewOrdersFilters filters)
        {
            var queryString = new StringBuilder();
            if (!string.IsNullOrEmpty(filters.orderType))
            {
                queryString.Append($"?orderType={filters.orderType}");
            }
            

            if (filters.from != DateTime.MinValue)
            {
                queryString.Append($"&from={filters.from?.ToString("yyyy-MM-dd")}");
            }

            if (filters.to != DateTime.MinValue)
            {
                queryString.Append($"&to={filters.to?.ToString("yyyy-MM-dd")}");
            }

            if (!string.IsNullOrEmpty(filters.productCode))
            {
                queryString.Append($"&productCode={filters.productCode}");
            }

            // Make the API call with the query string
           
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<viewOrdersVM>>>($"api/orders/getOrders{queryString}");
                if (response != null)
                    viewOrdersVMs = response.Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public async Task AddOrder(viewOrdersVM view)
        {
            var response = await _http.PostAsJsonAsync("api/orders/addOrder", view);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrder(long id)
        {
            var response = await _http.DeleteAsync($"api/orders/deleteOrder/{id}");
            viewOrdersVMs = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<viewOrdersVM>>>()).Data;
            //await GetOrders();
            OnChange.Invoke();
        }

        public async Task UpdateOrder(viewOrdersVM view)
        {
            var response = await _http.PutAsJsonAsync("api/orders/updateOrder", view);
            viewOrdersVMs = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<viewOrdersVM>>>()).Data;
            //await GetOrders();
            OnChange.Invoke();
        }

        public async Task UpdateOrderLine(OrderLineVM view)
        {
            var response = await _http.PutAsJsonAsync("api/orders/updateOrderLine", view);
            viewOrdersVMs = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<viewOrdersVM>>>()).Data;
            //await GetOrders();
            OnChange.Invoke();
        }
    }
}
