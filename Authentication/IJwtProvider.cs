using Microsoft.AspNetCore.Identity;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Authentication
{
    public interface IJwtProvider
    {
        public JwtTokenResultDto TokenGenerator(User user, IList<string> roles);
    }
}
