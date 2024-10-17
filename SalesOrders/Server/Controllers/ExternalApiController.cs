using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrders.Shared.ExternalCalls;
using SalesOrders.Shared.ExternalCalls.Models;
using SalesOrders.Shared.User;

namespace SalesOrders.Server.Controllers
{
    [Route("api/external")]
    [ApiController]
    [Authorize]
    public class ExternalApiController : ControllerBase
    {
        private readonly IExternalCallService _x;

        public ExternalApiController(IExternalCallService x)
        {
            _x = x;
        }

        [HttpGet]
        [Route("externalCall")]
        public async Task<ActionResult<ExternalCallVM>> CallDogApi()
        {
            try
            {
                var response = await _x.CallDogApi();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
