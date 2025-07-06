namespace user_management_api_dotnet8.DTOs
{
    public class UserCreateDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
    }
}
