namespace LegendaryDashboard.Contracts.Contracts.Advert.Requests
{
    public sealed class GetAdvertsRequest
    {
        public string Title { get; set; }

        public int? MinPrice { get; set; }

        public int? MaxPrice { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}