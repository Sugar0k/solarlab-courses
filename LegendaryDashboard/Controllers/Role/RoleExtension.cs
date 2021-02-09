namespace LegendaryDashboard.Controllers.Role
{
    public static class RoleExtension
    {
        public static Contracts.RoleDto.RoleDto ToDto(this Models.Role role)
        {
            return new()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}