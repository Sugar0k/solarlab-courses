namespace LegendaryDashboard.Contracts.Contracts.Advert.Requests
{
    public sealed class PagedAdvertsRequest
    {
        public int? OwnerId { get; set; }
        public int? FollowerId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}