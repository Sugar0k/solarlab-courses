using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
{
    
    public partial class CategoryController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound("Нет такой категории"); 
            }

            _db.Categories.Remove(category);
            _db.SaveChangesAsync();

            return NoContent();
        }
    }
}