using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            if (user == null)
                return BadRequest();
            await _userServices.CreateUserAsync(user);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,[FromBody]UserUpdateDto userUpdate)
        {
            if (userUpdate == null)
                return BadRequest("Invalid user data");
            await _userServices.UpdateUserAsync(id, userUpdate);
            return Ok(userUpdate);
        }

        [HttpGet("Get_All_Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userServices.GetUsersAsync();
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }


    }
}
