using FluentValidation;
using user_management_api_dotnet8.DTOs;

namespace user_management_api_dotnet8.Validators
{
    public class UpdateRequestValidation : AbstractValidator<UserUpdateDto>
    {
        public UpdateRequestValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .MinimumLength(6).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must be at least 6 characters long")
                .Matches("[A-Z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one digit")
                .Matches("[^a-zA-Z0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one special character");

        }
    }
}
