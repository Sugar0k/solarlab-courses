using Microsoft.AspNetCore.Mvc;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.Role.Requests;

namespace LegendaryDashboard.Api.Controllers.Role
{
    public partial class RoleController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpPost]
        public IActionResult Create(CreateRoleRequest request)
        {
            if (_db.Roles.Any(c => c.Name == request.Name))
            {
                return BadRequest("Роль с таким названием уже существует");
            }

            var newRole = new Domain.Models.Role
            {
                Name = request.Name
            };

            _db.Roles.Add(newRole);
            _db.SaveChangesAsync();

            return Created($"api/category/id/{newRole.Id}", newRole.ToDto());
        }
    }
}