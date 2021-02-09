using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    
    public partial class UserController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Models.User user = _db.Users.FirstOrDefault(c => c.Id == id);

            if (user == null)
            {
                return NotFound("There is no such user");    // Нет такого пользователя
            }

            _db.Users.Remove(user);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}