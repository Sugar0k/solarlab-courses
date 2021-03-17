using System;

namespace LegendaryDashboard.Domain.Models
{
    public class AdvertImage : BaseEntity<string>
    {
        public string FileName { get; set; } //Изначальное название
        public string FilePath { get; set; } //Путь к файлу
        public DateTime DateCreate { get; set; } //Дата создания
        public int AdvertId { get; set; }      // внешний ключ
        public Advert Advert { get; set; }      // навигационное свойство
    }
}