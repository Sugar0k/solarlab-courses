using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Advert;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController : ControllerBase
    {
        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> Update(
            UpdateAdvertsRequest request,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            await service.Update(request, cancellationToken);
            return Ok();
        }
    }
}