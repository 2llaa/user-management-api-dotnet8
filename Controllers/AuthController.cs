using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Services;

namespace user_management_api_dotnet8.Controllers
{
    [Route("api/[controller]")]

    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("LogIn")]
        [AllowAnonymous]

        public async Task<IActionResult>LogIn([FromBody]LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPost("SignUp")]
        [AllowAnonymous]

        public async Task<IActionResult> SignUp([FromBody]SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            try
            {
                var result = await _authService.SignUpAsync(signUpDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost("MakeAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] string email)
        {
           
            await _authService.ChangeToAdminAsync(email);
                return Ok("User is now an Admin");
           
        }

    }
}
