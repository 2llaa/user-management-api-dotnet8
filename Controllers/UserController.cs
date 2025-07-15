using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> CreateUser([FromBody] SignUpDto user)
        {
            if (user == null)
                return BadRequest();
           var createduser= await _userServices.CreateUserAsync(user);
            return CreatedAtAction("GetUserById", new { id = createduser.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id,[FromBody]UserUpdateDto userUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var user=await _userServices.UpdateUserAsync(id, userUpdate);
                return CreatedAtAction("GetUserById", new {id= user.UserId }, userUpdate);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("Get_All_Users")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]

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

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult>GetUserByID(string id)
        {
            try
            {
                var user = await _userServices.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await _userServices.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
