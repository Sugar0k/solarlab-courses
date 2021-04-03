using System;
using System.Collections.Generic;

namespace LegendaryDashboard.Contracts.Contracts.Advert.Requests
{
    public sealed class UpdateAdvertsRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}