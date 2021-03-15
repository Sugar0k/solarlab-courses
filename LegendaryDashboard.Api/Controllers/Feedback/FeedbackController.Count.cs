using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [AllowAnonymous]
        [HttpGet("count")]
        public async Task<IActionResult> Count(
            int userId,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            var count = await service.Count((f => f.UserId == userId), cancellationToken);
            return Ok(count);
        }
    }
}