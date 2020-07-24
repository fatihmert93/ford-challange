using System.Collections.Generic;
using System.Threading.Tasks;
using Ford.Models;
using Ford.Security.Bearer;

namespace Ford.Core.Abstracts.Services
{
    public interface IUserService
    {
        Task<JwtToken> Login(string username, string password);
        Task<bool> CreateUser(User user);
        Task<bool> CreateUserVehicle(int userId, string vin);
        Task<IEnumerable<string>> GetUserVehicles(int userId);
    }
}