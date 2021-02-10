namespace LegendaryDashboard.Domain.Models
{
    public class AdvertImage : BaseEntity
    {
        public string ImageGuid { get; set; }      // внешний ключ
        public Image Image { get; set; }      // навигационное свойство
        public int AdvertId { get; set; }      // внешний ключ
        public Advert Advert { get; set; }      // навигационное свойство
    }
}