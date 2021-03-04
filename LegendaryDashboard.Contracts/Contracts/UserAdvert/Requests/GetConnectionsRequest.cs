using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.UserAdvert.Requests
{
    public class GetConnectionsRequest
    {
        [Required(ErrorMessage = "User required ")] // необходим id пользователя
        public int Id { get; set; }
        // [Required(ErrorMessage = "ConnectionType required ")] // необходим тип связи
        // public string Type { get; set; }
        [Required(ErrorMessage = "Offset required ")] // необходим id пользователя
        public int Offset { get; set; }
        [Required(ErrorMessage = "Limit required ")] // необходим id пользователя
        public int Limit { get; set; }
    }
}