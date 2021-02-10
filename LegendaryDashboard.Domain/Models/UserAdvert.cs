namespace LegendaryDashboard.Domain.Models
{
    public class UserAdvert : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }

        public int TypeId { get; set; } //связь со списком Типов связей (напр. Мое, избранное, удаленное, закрытое)
        public AdvertConnectionType Type { get; set; }
    }
}