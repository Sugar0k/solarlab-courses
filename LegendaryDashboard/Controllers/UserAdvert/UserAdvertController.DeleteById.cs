using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.UserAdvert
{
    
    public partial class UserAdvertController
    {
        //TODO: Добавить Authorize(Roles = "admin") 
        // TODO: Добавить "хозяин объявления"
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Models.UserAdvert userAdvert = _db.UserAdverts.FirstOrDefault(c => c.Id == id);

            if (userAdvert == null)
            {
                return NotFound("There is no such user advert connection");    // Нет такой связи
            }

            _db.UserAdverts.Remove(userAdvert);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}