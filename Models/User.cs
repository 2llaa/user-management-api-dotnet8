using Microsoft.AspNetCore.Identity;

namespace user_management_api_dotnet8.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
