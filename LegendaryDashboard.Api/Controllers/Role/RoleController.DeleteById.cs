using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Role
{
    
    public partial class RoleController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Domain.Models.Role role = _db.Roles.FirstOrDefault(c => c.Id == id);

            if (role == null)
            {
                return NotFound("There is no such role");    // Нет такой роли
            }

            _db.Roles.Remove(role);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}