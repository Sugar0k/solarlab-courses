using System.Collections.Generic;

namespace LegendaryDashboard.Domain.Models
{
    public class Category : BaseEntity<int>
    {
        public string Title { get; set; }

        public int? ParentCategoryId { get; set; }      // внешний ключ
        public virtual Category ParentCategory { get; set; }      // навигационное свойство
        
        public virtual ICollection<Category> ChildCategories { get; set; } // список подкатегорий
        
        public virtual ICollection<Advert> Adverts { get; set; } // список объявлений
    }
}