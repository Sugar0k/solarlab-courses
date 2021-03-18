using AutoMapper;
using LegendaryDashboard.Contracts.Contracts.AdvertImage;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.MapperProfiles
{
    public class AdvertImageMapperProfile : Profile
    {
        public AdvertImageMapperProfile()
        {
            CreateMap<AdvertImage, AdvertImageDto>();
        }
    }
}