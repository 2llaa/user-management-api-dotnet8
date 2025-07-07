using AutoMapper;
using user_management_api_dotnet8.DTOs;
using user_management_api_dotnet8.Models;

namespace user_management_api_dotnet8.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            //source->target
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.Password));


            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.HashPassword, opt => opt.MapFrom(src => src.Password));

            CreateMap<User, UserReadDto>();
            CreateMap<UserReadDto,User>();
        }
    }
}
