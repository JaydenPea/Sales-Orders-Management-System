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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orders, ILogger<OrdersController> logger)
        {
            _orderService = orders;
            _logger = logger;
        }
            
        [HttpGet]
        [Route("getOrders")]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> GetOrders([FromQuery] viewOrdersFilters filters)
        {
            _logger.LogInformation("GetOrders called with filters: {Filters}", filters);
            try
            {
                var response = await _orderService.GetOrders(filters);
                _logger.LogInformation("GetOrders completed successfully with {OrderCount} orders.", response.Data.Count);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting orders with filters: {Filters}", filters);
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

        [HttpPut("updateOrder"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> UpdateOrder(viewOrdersVM order)
        {
            try
            {
                // Check if the user is an Admin
                if (!User.IsInRole("Admin"))
                {
                    return Forbid("Only Admin can perform these tasks");
                }

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

        [HttpPut("updateOrderLine"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<viewOrdersVM>>>> UpdateLineOrders(OrderLineVM order)
        {
            try
            {
                var response = await _orderService.UpdateLineOrders(order);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getOrderTypeStats")]
        public async Task<ActionResult<ServiceResponse<OrderTypeStatsVM>>> OrderTypeCount()
        {
            try
            {
                var response = await _orderService.OrderTypeCount();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getHeaderStats")]
        public async Task<ActionResult<ServiceResponse<OrderHeaderStatsVM>>> OrderHeaderStats()
        {
            try
            {
                var response = await _orderService.OrderHeaderStats();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
