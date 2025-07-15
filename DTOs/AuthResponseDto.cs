namespace user_management_api_dotnet8.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
