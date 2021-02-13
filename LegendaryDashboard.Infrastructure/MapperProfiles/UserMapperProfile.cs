using System.Collections.Generic;
using LegendaryDashboard.Domain.Models;
using AutoMapper;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Contracts.Contracts.User.Requests;

namespace LegendaryDashboard.Infrastructure.MapperProfiles
{
   

    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserDto>();
            CreateMap<List<CreateUserRequest>, List<User>>();
            CreateMap<List<User>, List<UserDto>>();
        }
    }
}