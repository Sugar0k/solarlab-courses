using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.Role.Requests
{
    public class CreateRoleRequest
    {
        [Required(ErrorMessage = "Name required ")] // необходимо имя роли
        public string Name { get; set; }
    }
}