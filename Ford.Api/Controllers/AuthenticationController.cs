using System;
using System.Threading.Tasks;
using Ford.Core.Abstracts.Services;
using Ford.Models.RequestResponseModels;
using Ford.Security.Bearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ford.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {

            try
            {
                JwtToken token = await _userService.Login(request.Username, request.Password);
                
                LoginResponseModel response = new LoginResponseModel();

                response.Token = token.Value;
                response.ExpiryTime = token.ValidTo;
                response.Status = true;
                response.Data = request.Username;
            
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = new LoginResponseModel()
                {
                    ErrorMessage = e.Message,
                    Status = false
                };

                return BadRequest(response);
            }

        }
        
    }
}