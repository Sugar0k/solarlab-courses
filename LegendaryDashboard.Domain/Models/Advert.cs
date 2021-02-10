using System;
using System.Collections.Generic;

namespace LegendaryDashboard.Domain.Models
{
    public class Advert : BaseEntity
    {
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
       
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public DateTime CreationDate { get; set; }
        
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        
        public int Views { get; set; }

        public List<AdvertImage> AdvertImages { get; set; } //связь со списком изображений
        
        public List<UserAdvert> UsersAdverts { get; set; } //связь со списком Пользователей
    }
}