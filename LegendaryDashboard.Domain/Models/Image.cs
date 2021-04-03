using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Domain.Models
{
    public class Image : BaseEntity<string>
    {
        //string id Уникальный номер + расширение 
        public string FileName { get; set; } //Изначальное название
        
        public string FilePath { get; set; } //Путь к файлу

        public ICollection<AdvertImage> ImageAdverts { get; set; } = new HashSet<AdvertImage>(); //связь со списком объявлений
    }
}