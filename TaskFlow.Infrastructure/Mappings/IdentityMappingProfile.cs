﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskFlow.Application.User;

namespace TaskFlow.Infrastructure.Mappings
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<IdentityUser, UserDto>();
        }
    }
}