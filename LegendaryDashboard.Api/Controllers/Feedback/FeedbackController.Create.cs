using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(
            [FromBody] FeedbackCreateRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _feedbackService.Create(request, cancellationToken));
        }
    }
}