using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpGet("id")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            var feedbacks = await service.GetById(id, cancellationToken);
            return Ok(feedbacks);
        }
    }
}