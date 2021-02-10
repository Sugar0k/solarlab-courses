using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Domain.Models
{
    public class Image
    {
        public string FileName { get; set; } //Изначальное название
        public string Guid { get; set; } //Уникальный номер + расширение 
        public string FilePath { get; set; } //Путь к файлу

        public AdvertImage AdvertImage { get; set; } //связь со списком объявлений
    }
}