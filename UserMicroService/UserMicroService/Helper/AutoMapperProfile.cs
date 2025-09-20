using AutoMapper;
using UserMicroService.DTOS;
using UserMicroService.Models;

namespace UserMicroService.Helper
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToUpper()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Trim().ToLower()))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Trim().ToLower()));
            CreateMap<User,UserSendDto>();
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Trim().ToUpper()));
        }
    }
}
