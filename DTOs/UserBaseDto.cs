namespace user_management_api_dotnet8.DTOs
{
    public abstract class UserBaseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
