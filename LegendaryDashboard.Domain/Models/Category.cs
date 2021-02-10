using System.Collections.Generic;

namespace LegendaryDashboard.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }
        
        public List<Category> ChildCategories { get; set; } // список подкатегорий
        
        public int? ParentCategoryId { get; set; }      // внешний ключ
        public Category ParentCategory { get; set; }      // навигационное свойство
    }
}