using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController : ControllerBase
    {
        [HttpPost]
        [Route("get/paged")] //limit='{limit}'_offset='{offset}'
        public async Task<IActionResult> GetByPaged(
            PagedAdvertsRequest request,
            CancellationToken cancellationToken)
        {
            
            return Ok(await _advertService.GetPaged(request, cancellationToken));
        }
    }
}