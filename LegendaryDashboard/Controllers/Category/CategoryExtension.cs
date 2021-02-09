using LegendaryDashboard.Contracts.CategoryDto;

namespace LegendaryDashboard.Controllers.Category
{
    public static class CategoryExtension
    {
        public static CategoryDto ToDto(this Models.Category category)
        {
            return new()
            {
                Id = category.Id,
                Title = category.Title
            };
        }
    }
}