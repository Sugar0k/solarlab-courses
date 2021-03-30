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
        [Route("update/view")]
        public async Task<IActionResult> AddView(
            int id,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            await service.AddView(id, cancellationToken);
            return Ok();
        }
    }
}