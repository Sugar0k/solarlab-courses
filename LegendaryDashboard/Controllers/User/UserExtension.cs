using LegendaryDashboard.Contracts.User;

namespace LegendaryDashboard.Controllers.User
{
    public static class UserExtension
    {
        public static UserDto ToDto(this Models.User user)
        {
            return new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                RoleId = user.RoleId
            };
        }
    }
}