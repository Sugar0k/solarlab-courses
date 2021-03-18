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
        [Route("get/id/{id}")]
        public async Task<IActionResult> GetById(
            int id,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            return Ok(await service.FindById(id, cancellationToken));
        }
    }
}