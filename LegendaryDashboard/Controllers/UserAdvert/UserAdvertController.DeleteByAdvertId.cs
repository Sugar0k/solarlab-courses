using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.UserAdvert
{
    
    public partial class UserAdvertController
    {
        //TODO: Добавить Authorize(Roles = "admin") 
        // TODO: Добавить "хозяин объявления"
        [HttpDelete("advert/{id}")]
        public IActionResult DeleteByAdvert(int id)
        {
            IEnumerable<Models.UserAdvert> userAdverts = _db.UserAdverts.Where(c => c.AdvertId == id);

            _db.UserAdverts.RemoveRange(userAdverts);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}