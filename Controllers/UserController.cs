using Microsoft.AspNetCore.Mvc;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Services;

namespace user_management_api_dotnet8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task <IActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            if (user == null)
                return BadRequest();
            await _userServices.CreateUserAsync(user);
            return Ok(user);
        }
    }
}
