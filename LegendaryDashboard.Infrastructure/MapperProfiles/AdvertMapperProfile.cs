using LegendaryDashboard.Domain.Models;
using AutoMapper;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;

namespace LegendaryDashboard.Infrastructure.MapperProfiles
{
   

    public class AdvertMapperProfile : Profile
    {
        public AdvertMapperProfile()
        {
            CreateMap<CreateAdvertRequest, Advert>();
            CreateMap<Advert, AdvertDto>();
        }
    }
}