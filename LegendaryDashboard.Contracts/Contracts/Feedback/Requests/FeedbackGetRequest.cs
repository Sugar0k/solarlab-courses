using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.Feedback.Requests
{
    public class FeedbackGetRequest
    {
        [Required(ErrorMessage = "Id required ")] // необходим идентификатор пользователя 
        public int UserId { get; set; }

        [Required(ErrorMessage = "How much to skip required ")] // Сколько пропустить
        public int Offset { get; set; }

        [Required(ErrorMessage = "How much to take required")] // Сколько взять
        public int Limit { get; set; }
    }
}