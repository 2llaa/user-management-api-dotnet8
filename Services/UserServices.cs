using AutoMapper;
using Azure.Messaging;
using Microsoft.EntityFrameworkCore;
using user_management_api_dotnet8.Data;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public UserServices(IMapper mapper , AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<Guid> CreateUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
                throw new Exception("there is no content");
            
            var user= _mapper.Map<User>(userDto);
            user.IsActive = true;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
            
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
