using System;

namespace LegendaryDashboard.Contracts.Contracts.AdvertImage
{
    public class AdvertImageDto
    {
        public string id { get; set; } 
        public string FileName { get; set; } //Изначальное название
        public byte[] data { get; set; }       //Данные
    }
}