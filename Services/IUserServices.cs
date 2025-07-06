using System.Runtime.CompilerServices;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public interface IUserServices
    {
         Task<IEnumerable<UserReadDto>> GetUsersAsync();
         Task<User> GetUserByIdAsync(int id);
         Task<int> CreateUserAsync(UserCreateDto userDto);
        Task<UserUpdateDto> UpdateUserAsync(int id,UserUpdateDto userUpdate);
    }
}
