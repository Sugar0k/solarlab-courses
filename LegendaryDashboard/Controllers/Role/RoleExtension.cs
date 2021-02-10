using LegendaryDashboard.Contracts.Contracts.Role;

namespace LegendaryDashboard.Controllers.Role
{
    public static class RoleExtension
    {
        public static RoleDto ToDto(this Domain.Models.Role role)
        {
            return new()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}