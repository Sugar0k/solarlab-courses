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
        [Route("create")]
        public async Task<IActionResult> Create(
            [FromBody] FeedbackCreateRequest request,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            await service.Create(request, cancellationToken);
            return Ok();
        }
    }
}