using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;
using user_management_api_dotnet8.DTOs;

namespace user_management_api_dotnet8.Validators
{
    public class LoginRequestValidation : AbstractValidator<LoginDto>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
