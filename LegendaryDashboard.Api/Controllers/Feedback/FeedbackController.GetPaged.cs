using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            int id, int offset, int limit,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            var feedbacks = await service.GetPaged(id, offset, limit, cancellationToken);
            return Ok(feedbacks);
        }
    }
}