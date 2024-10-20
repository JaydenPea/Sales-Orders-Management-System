using Microsoft.AspNetCore.Mvc;
using SalesOrders.Client.Service.AuthService;
using SalesOrders.DAL.Models;
using SalesOrders.Shared;
using SalesOrders.Shared.User;
using SalesOrders.Shared.User.Models;
using SalesOrders.Shared.Users.Models;

namespace SalesOrders.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authService;

        public UserController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
        {
            try
            {
                var response = await _authService.Register(
               new SalesOrders.DAL.Models.Users
               {
                   email = request.email
               },
               request.password);

                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            try
            {
                var response = await _authService.Login(request.Email, request.Password);
                if (!response.Success)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("getUsersList")]
        public async Task<ActionResult<List<GetUsersModel>>> GetAllUsers()
        {
            try
            {
                var response = await _authService.GetAllUsers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
 