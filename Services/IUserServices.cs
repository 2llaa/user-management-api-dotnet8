using System.Runtime.CompilerServices;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public interface IUserServices
    {
         Task<IEnumerable<User>> GetUsersAsync();
         Task<User> GetUserByIdAsync(Guid id);
        Task<Guid> CreateUserAsync(UserCreateDto userDto);
    }
}
