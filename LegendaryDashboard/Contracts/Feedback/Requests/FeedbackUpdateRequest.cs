using System.ComponentModel.DataAnnotations;

namespace LegendaryDashboard.Contracts.Feedback.Requests
{
    public class FeedbackUpdateRequest
    {
        [Required(ErrorMessage = "Id required ")] // Необходим Id нужного комментария
        public int Id { get; set; }

        [Required(ErrorMessage = "Text required")] // Необходим текс комментария
        public string Text { get; set; }

        [Required(ErrorMessage = "Rating required ")] // необходима оценка от пользователя
        public byte Rating { get; set; }

    }
}