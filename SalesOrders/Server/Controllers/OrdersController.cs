using Microsoft.AspNetCore.Mvc;
using SalesOrders.Shared.ExternalCalls.Models;
using SalesOrders.Shared.ExternalCalls;
using SalesOrders.Shared.Orders;
using SalesOrders.Shared;
using SalesOrders.Shared.Orders.Models;
using Microsoft.AspNetCore.Authorization;

namespace SalesOrders.Server.Controllers
{
    [Route("api/orders")]
    [ApiController]
    //[Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orders)
        {
            _orderService = orders;
        }

        [HttpGet]
        [Route("getOrders")]
        public async Task<ActionResult<viewOrdersVM>> GetOrders([FromQuery] viewOrdersFilters filters)
        {
            try
            {
                var response = await _orderService.GetOrders(filters);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
