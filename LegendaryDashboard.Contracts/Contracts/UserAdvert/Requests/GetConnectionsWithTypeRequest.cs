using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests
{
    public class GetConnectionsWithTypeRequest
    {
        [Required(ErrorMessage = "User required ")] // необходим id
        public int Id { get; set; }
        [Required(ErrorMessage = "ConnectionType required ")] // необходим тип связи
        public string Type { get; set; }
        [Required(ErrorMessage = "Offset required ")] // необходим отступ пользователя
        public int Offset { get; set; }
        [Required(ErrorMessage = "Limit required ")] // необходимо ограничение пользователя
        public int Limit { get; set; }
    }
}