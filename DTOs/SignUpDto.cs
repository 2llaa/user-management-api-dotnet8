namespace user_management_api_dotnet8.DTOs
{
    public class SignUpDto: UserBaseDto
    {
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
