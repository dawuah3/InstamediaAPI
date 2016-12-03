using AutoMapper;
using InstamediaApi.Dtos;
using InstamediaApi.Models;

namespace InstamediaApi.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();
        }
    }
}