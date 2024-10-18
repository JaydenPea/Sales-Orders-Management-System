using Microsoft.AspNetCore.Mvc;
using SalesOrders.Shared.ExternalCalls.Models;
using SalesOrders.Shared.ExternalCalls;
using SalesOrders.Shared.Orders;
using SalesOrders.Shared;
using SalesOrders.Shared.Orders.Models;
using Microsoft.AspNetCore.Authorization;
using MudBlazor.Charts;

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
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> GetOrders([FromQuery] viewOrdersFilters filters)
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

        [HttpDelete("deleteOrder/{id}"),Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> Delete(long id)
        {
            try
            {
                var response = await _orderService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateOrder"), /*Authorize(Roles = "Admin")*/]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> UpdateOrder(viewOrdersVM order)
        {
            try
            {
                var response = await _orderService.UpdateOrder(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addOrder"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> AddOrder(viewOrdersVM order)
        {
            try
            {
                var response = await _orderService.AddOrder(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
