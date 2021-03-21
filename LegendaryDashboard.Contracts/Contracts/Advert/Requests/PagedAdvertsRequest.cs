namespace LegendaryDashboard.Contracts.Contracts.Advert.Requests
{
    public sealed class PagedAdvertsRequest
    {
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public int? CategoryId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Title { get; set; }
        public int? OwnerId { get; set; }
        public int? FollowerId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}