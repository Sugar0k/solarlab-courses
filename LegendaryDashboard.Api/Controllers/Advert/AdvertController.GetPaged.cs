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
        [Route("get/paged/offset='{request.offset}'_limit='{request.limit}'")]
        public async Task<IActionResult> GetByPaged(
            PagedAdvertsRequest request,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetPaged(request, cancellationToken));
        }
    }
}