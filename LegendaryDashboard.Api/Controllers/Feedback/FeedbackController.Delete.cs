﻿using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.FeedbackService.Interfaces;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Feedback
{
    public partial class FeedbackController
    {
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IFeedbackService service,
            CancellationToken cancellationToken)
        {
            await service.Delete(id, cancellationToken);
            return Ok();
        }
    }
}