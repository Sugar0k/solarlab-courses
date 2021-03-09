using System.Collections.Generic;

namespace LegendaryDashboard.Contracts.Contracts.Category
{
    public sealed class CategoryListDto
    {
        public CategoryListDto(int count, List<CategoryDto> categories)
        {
            Count = count;
            Categories = categories;
        }

        public int Count { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}