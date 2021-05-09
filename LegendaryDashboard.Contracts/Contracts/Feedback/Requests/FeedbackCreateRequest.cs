using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Contracts.Feedback.Requests
{
    public class FeedbackCreateRequest
    {
        [Required(ErrorMessage = "UserId required ")] // необходим идентификатор пользователя которого комментируют
        public int UserId { get; set; }

        public string Text { get; set; }

        [Required(ErrorMessage = "Rating required ")] // необходима оценка от пользователя
        public byte Rating { get; set; }

    }
}