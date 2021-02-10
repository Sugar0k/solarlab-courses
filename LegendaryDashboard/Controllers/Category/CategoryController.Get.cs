using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.Category;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Category
{
    public partial class CategoryController
    {
        [HttpGet("{id}")]
        public CategoryDto Get(int id)
        {
            return _db.Categories.FirstOrDefault(c => c.Id == id).ToDto();
        }
        
        [HttpGet("all")]
        public List<CategoryDto> Get()
        {
            return _db.Categories.Select(x => x.ToDto()).ToList();
        }
        
        /// <summary>
        /// Получить список дочерних категорий
        /// </summary>
        [HttpGet("children/{id}")]
        public List<CategoryDto> GetChildCategories(int id)
        {
            return _db.Categories.Select(x => x.ToDto()).ToList();
        }
        
    }
}