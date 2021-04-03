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
        [HttpPatch("update")]
        public async Task<IActionResult> Update(
            [FromBody] FeedbackUpdateRequest request,
            CancellationToken cancellationToken)
        {
            await _feedbackService.Update(request, cancellationToken);
            return Ok();
        }
    }
}