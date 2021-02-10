using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using LegendaryDashboard.Api.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpPost]
        [Route("create")]
        public IActionResult Create(FeedbackCreateRequest createRequest)
        {
            var feedback = new Domain.Models.Feedback
            {
                UserId = createRequest.UserId,
                //TODO: будет взято из контекста
                CommentatorId = createRequest.CommentatorId,
                Text = createRequest.Text,
                Rating = createRequest.Rating
            };
            
            _db.Feedbacks.Add(feedback);
            _db.SaveChangesAsync();
            
            return Ok();
        }
    }
}