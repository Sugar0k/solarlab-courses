using System.Linq;
using LegendaryDashboard.Contracts.Category.Requests;
using LegendaryDashboard.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Category
{
    public partial class CategoryController
    {
        //TODO: Добавить Authorize(Roles = "admin")
        [HttpPost]
        public IActionResult Create(CreateCategoryRequest request)
        {
            if (_db.Categories.Any(c => c.Title == request.Title))
            {
                return BadRequest("Элемент с таким названием уже существует");    
            }

            var newCategory = new Models.Category
            {
                Title = request.Title,
                ParentCategoryId = request.ParentCategoryId
            };

            _db.Categories.Add(newCategory);
            _db.SaveChangesAsync();
            
            return Created($"api/category/id/{newCategory.Id}", newCategory.ToDto());
        }
    
    }
}