using LegendaryDashboard.Contracts.Contracts.UserAdvert;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    public static class UserAdvertExtension
    {
        public static UserAdvertDto ToDto(this Domain.Models.UserAdvert ua)
        {
            return new()
            {
                Id = ua.Id,
                UserId = ua.UserId,
                AdvertId = ua.AdvertId,
                TypeId = ua.TypeId
            };
        }
    }
}