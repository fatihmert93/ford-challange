using System.Threading.Tasks;
using Ford.Core.Abstracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ford.Api.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IUserService _userService;

        public VehiclesController(IUserService userService)
        {
            _userService = userService;
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserVehicles(int userId)
        {
            var result = await _userService.GetUserVehicles(userId);
            return Ok(result);
        }
    }
}