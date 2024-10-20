using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrders.DAL.Models;
using SalesOrders.Shared.User.Models;

namespace SalesOrders.Shared.User
{
    public interface IUserService
    {
        Task<ServiceResponse<int>> Register(SalesOrders.DAL.Models.Users user, string passwword);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<SalesOrders.DAL.Models.Users> GetUserByEmail(string email);

        //User management
        Task<List<GetUsersModel>> GetAllUsers();
    }
}
