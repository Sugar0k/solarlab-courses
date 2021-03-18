using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController : ControllerBase
    {
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count(
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            return Ok(await service.Count(cancellationToken));
        }
    }
}