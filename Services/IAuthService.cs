using user_management_api_dotnet8.DTOs;

namespace user_management_api_dotnet8.Services
{
    public interface IAuthService
    {
        
        public Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        public Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto);
        public Task ChangeToAdminAsync(string email);

    }
}
