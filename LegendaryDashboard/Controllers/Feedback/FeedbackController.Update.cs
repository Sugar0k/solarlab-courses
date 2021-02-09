using System.Linq;
using LegendaryDashboard.Contracts.Feedback.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpPatch]
        [Route("update")]
        public IActionResult Update(FeedbackUpdateRequest request)
        {
            var feedback = _db.Feedbacks.First(c => c.Id == request.Id);
            if (feedback != null)
            {
                feedback.Text = request.Text;
                feedback.Rating = request.Rating;
                _db.Feedbacks.Update(feedback);
                _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}