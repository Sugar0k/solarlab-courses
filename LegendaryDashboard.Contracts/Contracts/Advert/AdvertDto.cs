using System;
using System.Collections.Generic;

namespace LegendaryDashboard.Contracts.Contracts.Advert
{
    public sealed class AdvertDto
    {
        //TODO: а что с изображениями?
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Views { get; set; }
        public IEnumerable<string> Images { get; set; }
    }
}