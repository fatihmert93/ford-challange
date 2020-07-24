using System.Threading.Tasks;
using Ford.Core.Abstracts.Services;
using Ford.Models;
using Ford.Models.RequestResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ford.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddVehicleToUser(AddVehicleToUserRequestModel request)
        {
            var result = await _userService.CreateUserVehicle(request.UserId,request.Vin);
            
            return Ok(result);
        }
    }
}