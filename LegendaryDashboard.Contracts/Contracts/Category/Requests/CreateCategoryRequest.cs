namespace LegendaryDashboard.Contracts.Category.Requests
{
    public sealed class CreateCategoryRequest
    {
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}