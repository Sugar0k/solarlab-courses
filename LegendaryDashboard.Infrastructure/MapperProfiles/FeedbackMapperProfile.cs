using AutoMapper;
using LegendaryDashboard.Contracts.Contracts.Feedback;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Domain.Models;

namespace LegendaryDashboard.Infrastructure.MapperProfiles
{
    public class FeedbackMapperProfile : Profile
    {
        public FeedbackMapperProfile()
        {
            CreateMap<FeedbackCreateRequest, Feedback>();
            CreateMap<Feedback, FeedbackDto>();
        }
    }
}