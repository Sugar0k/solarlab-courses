using System.Linq;
using LegendaryDashboard.Contracts.Contracts.Category.Requests;
using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Category
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

            var newCategory = new Domain.Models.Category
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