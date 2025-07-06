namespace user_management_api_dotnet8.DTOs
{
    public class UserUpdateDto: UserBaseDto
    {
        public string Password { get; set; } = string.Empty;

    }
}
