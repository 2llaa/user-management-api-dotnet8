using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using user_management_api_dotnet8.Authentication;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public AuthService
            (UserManager<User> userManager,
            SignInManager<User> signInManager,
            IJwtProvider jwtProvider,
            IMapper mapper)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            //check if user saved in db by checking email&password
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null)
                throw new UnauthorizedAccessException("Wrong Email or Password");
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!IsPasswordValid)
                throw new UnauthorizedAccessException("Wrong Email or Password");
            //generate token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtProvider.TokenGenerator(user, roles);
            //return generated token
            return new AuthResponseDto
            {
                Email = user.Email!,
                Token = token.Token

            };
        }

        public async Task<AuthResponseDto> SignUpAsync(SignUpDto signUpDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(signUpDto.Email);
            var userName = await _userManager.FindByNameAsync(signUpDto.UserName);

            if (userEmail != null)
                throw new Exception("Email already exists");
            if (userName != null)
                throw new Exception("Username already exists");

            var newUser = new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                UserName = signUpDto.UserName,
                Email = signUpDto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, signUpDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new Exception("User creation failed: " + errors);
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            var roles = await _userManager.GetRolesAsync(newUser);
            var token = _jwtProvider.TokenGenerator(newUser, roles);

            return new AuthResponseDto
            {
                Email = newUser.Email!,
                Token = token.Token
            };
        }

        public async Task ChangeToAdminAsync (string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new UnauthorizedAccessException("wrong");

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.AddToRoleAsync(user, "Admin");
        }

    }
}
