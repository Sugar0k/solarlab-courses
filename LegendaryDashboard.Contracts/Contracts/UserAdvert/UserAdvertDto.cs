namespace LegendaryDashboard.Contracts.Contracts.UserAdvert
{
    public class UserAdvertDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AdvertId { get; set; }
        public int TypeId { get; set; } //связь со списком Типов связей (напр. Мое, избранное, удаленное, закрытое)
    }
}