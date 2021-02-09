using LegendaryDashboard.Contracts.UserAdvert;

namespace LegendaryDashboard.Controllers.UserAdvert
{
    public static class UserAdvertExtension
    {
        public static UserAdvertDto ToDto(this Models.UserAdvert ua)
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