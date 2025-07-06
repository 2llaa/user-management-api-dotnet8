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
        public async Task<int> CreateUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
                throw new Exception("there is no content");
            
            var user= _mapper.Map<User>(userDto);
            user.IsActive = true;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
            
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
           var users=await _dbContext.Users.Where(u=>u.IsActive)
                .ToListAsync();
            if (!users.Any())
                throw new KeyNotFoundException("there is no users");

            return _mapper.Map<IEnumerable<UserReadDto>>(users);

        }

        public async Task<UserUpdateDto> UpdateUserAsync(int id,UserUpdateDto userUpdate)
        {
            var user =await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId==id);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            _mapper.Map(userUpdate,user);
           await _dbContext.SaveChangesAsync();
            return userUpdate;
        }
    }
}
