using LegendaryDashboard.Domain.Common;

namespace LegendaryDashboard.Domain.Models
{
    public class UserAdvert : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }

        public string ConnectionType { get; set; } //связь со списком Типов связей (напр. Мое, избранное
    }
}