using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var feedback = _db.Feedbacks.First(c => c.Id == id);
            if (feedback != null)
            {
                _db.Feedbacks.Remove(feedback);
                _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}