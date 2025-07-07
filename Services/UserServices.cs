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
        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
                throw new Exception("there is no content");
            
            var user= _mapper.Map<User>(userDto);
            user.IsActive = true;
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
            
        }

        public async Task DeleteUserAsync(int id)
        {
           var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);
            if (user is null)
                throw new KeyNotFoundException("User not found or already deleted");
            user.IsActive = false;
           await _dbContext.SaveChangesAsync();

        }

        public async Task<UserReadDto> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive);
            if (user is null)
                throw new KeyNotFoundException("user not found");
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersAsync()
        {
           var users=await _dbContext.Users.Where(u=>u.IsActive)
                .ToListAsync();
            if (!users.Any())
                throw new KeyNotFoundException("there is no users");

            return _mapper.Map<IEnumerable<UserReadDto>>(users);

        }

        public async Task<UserReadDto> UpdateUserAsync(int id,UserUpdateDto userUpdate)
        {
            var user =await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId==id&&u.IsActive);
            if (user == null)
                throw new KeyNotFoundException("User not found.");
            _mapper.Map(userUpdate,user);
           await _dbContext.SaveChangesAsync();
           return _mapper.Map<UserReadDto>(user);
        }
    }
}
