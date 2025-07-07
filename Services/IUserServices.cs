using System.Runtime.CompilerServices;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public interface IUserServices
    {
         Task<IEnumerable<UserReadDto>> GetUsersAsync();
         Task<UserReadDto> GetUserByIdAsync(int id);
         Task<UserReadDto> CreateUserAsync(UserCreateDto userDto);
         Task<UserReadDto> UpdateUserAsync(int id,UserUpdateDto userUpdate);
         Task DeleteUserAsync(int id);
    }
}
