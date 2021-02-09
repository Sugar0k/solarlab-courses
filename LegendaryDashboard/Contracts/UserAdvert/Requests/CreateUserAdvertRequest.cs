using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LegendaryDashboard.Contracts.UserAdvert.Requests
{
    public class CreateUserAdvertRequest
    {
        [Required(ErrorMessage = "User required ")] // необходим id пользователя
        public int UserId { get; set; }
        [Required(ErrorMessage = "Advert required ")] // необходим id объявления
        public int AdvertId { get; set; }
        [Required(ErrorMessage = "ConnectionType required ")] // необходим тип связи
        public int TypeId { get; set; }
    }
}