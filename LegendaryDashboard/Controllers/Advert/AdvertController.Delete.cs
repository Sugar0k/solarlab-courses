using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Advert
{

    public partial class AdvertController
    {
        //TODO: Добавить Authorize(Roles = "admin") и хозяин собственно
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var advert = _db.Adverts.FirstOrDefault(c => c.Id == id);

            if (advert == null)
            {
                return NotFound("There is no such advert");    // Нет такого объявления
            }

            _db.Adverts.Remove(advert);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
} 