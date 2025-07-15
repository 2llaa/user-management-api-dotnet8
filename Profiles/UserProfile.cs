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
            CreateMap<SignUpDto, User>();

            //CreateMap<JwtTokenResultDto, AuthResponseDto>();

            CreateMap<UserUpdateDto, User>();

            CreateMap<User, UserReadDto>()
                  .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)); ;
            CreateMap<UserReadDto,User>();
        }
    }
}
