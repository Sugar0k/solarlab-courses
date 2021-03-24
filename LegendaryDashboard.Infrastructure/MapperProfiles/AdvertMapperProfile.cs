
using LegendaryDashboard.Domain.Models;
using AutoMapper;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using LegendaryDashboard.Contracts.Contracts.User.Requests;

namespace LegendaryDashboard.Infrastructure.MapperProfiles
{

    public class AdvertMapperProfile : Profile
    {
        public AdvertMapperProfile()
        {
            CreateMap<CreateAdvertRequest, Advert>();
            CreateMap<Advert, AdvertDto>();
            CreateMap<UpdateAdvertsRequest, Advert>();
        }
    }
}