﻿using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.Advert.Requests
{
    public sealed class CreateAdvertRequest
    {
        //TODO: А что с изображениями?
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int? Price { get; set; }
        
        [Required]
        public string State { get; set; }
        
        [Required]
        public string City { get; set; }
        
        public string Street { get; set; }
        
        public string House { get; set; }

    }
}