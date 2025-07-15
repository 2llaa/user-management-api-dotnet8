namespace user_management_api_dotnet8.Validators
{
    using FluentValidation;
    using global::user_management_api_dotnet8.DTOs;

    namespace user_management_api_dotnet8.Validators
    {
        public class SignupRequestValidation : AbstractValidator<SignUpDto>
        {
            public SignupRequestValidation()
            {
                RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(4).WithMessage("Username must be at least 4 characters long");

                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format");

                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                    .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

                RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("First name is required");

                RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("Last name is required");
            }
        }
    }

}
