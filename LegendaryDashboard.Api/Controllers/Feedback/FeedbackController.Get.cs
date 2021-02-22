using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(
            [FromBody] FeedbackGetRequest request,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            var feedbacks = await service.Get(request, cancellationToken);
            return Ok(feedbacks);
        }
    }
}