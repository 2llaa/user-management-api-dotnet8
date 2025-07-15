namespace user_management_api_dotnet8.DTOs
{
    public class JwtTokenResultDto
    { 
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
