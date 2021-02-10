using LegendaryDashboard.Contracts.Contracts.Category;

namespace LegendaryDashboard.Controllers.Category
{
    public static class CategoryExtension
    {
        public static CategoryDto ToDto(this Domain.Models.Category category)
        {
            return new()
            {
                Id = category.Id,
                Title = category.Title
            };
        }
    }
}