using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Feedback.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    // public partial class FeedbackController
    // {
    //     [HttpPatch]
    //     [Route("update")]
    //     public async Task<IActionResult> Update(
    //         [FromBody] FeedbackUpdateRequest request,
    //         [FromServices] IFeedbackService service,
    //         CancellationToken cancellationToken)
    //     {
    //         await service.Update(request, cancellationToken);
    //         return Ok();
    //     }
    // }
}