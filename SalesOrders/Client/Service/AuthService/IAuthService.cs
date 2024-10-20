using SalesOrders.Shared.Users.Models;
using SalesOrders.Shared;
using SalesOrders.Shared.User.Models;

namespace SalesOrders.Client.Service.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<bool> IsUserAuthenticated();

        Task<List<GetUsersModel>> GetAllUsers();

        event Action OnChange; 

    }
}
