using AutoMapper;
using core.DTO.UserDTO;
using core.Entities;

namespace core.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        this.CreateMap<User, UserDto>()
            .ReverseMap();
        
        this.CreateMap<UserSignUp, User>()
            .ReverseMap();
    }
}