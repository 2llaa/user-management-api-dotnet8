﻿using System.Runtime.CompilerServices;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public interface IUserServices
    {
         Task<IEnumerable<UserReadDto>> GetUsersAsync();
         Task<UserReadDto> GetUserByIdAsync(string id);
         Task<UserReadDto> CreateUserAsync(SignUpDto userDto);
         Task<UserReadDto> UpdateUserAsync(string id,UserUpdateDto userUpdate);
         Task DeleteUserAsync(string id);
    }
}
