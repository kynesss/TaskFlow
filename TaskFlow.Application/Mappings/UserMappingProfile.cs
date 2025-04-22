using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskFlow.Application.User;

namespace TaskFlow.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }
}