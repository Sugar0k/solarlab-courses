using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Feedback.Requests
{
    public class FeedbackGetRequest
    {
        [Required(ErrorMessage = "Id required ")] // необходим идентификатор пользователя которого комментируют
        public int UserId { get; set; }

        [Required(ErrorMessage = "How much to take required ")] // необходим идентификатор комментатора
        public int ToTake { get; set; }

        [Required(ErrorMessage = "How much to skip required")] // Необходим текс комментария
        public int ToSkip { get; set; }
    }
}