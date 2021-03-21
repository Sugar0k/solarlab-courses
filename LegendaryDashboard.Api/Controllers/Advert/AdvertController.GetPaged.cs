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
        [Route("get/paged")] //limit='{limit}'_offset='{offset}'
        public async Task<IActionResult> GetByPaged(
            int? OwnerId,
            int? FollowerId,
            int Limit,
            int Offset,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            
            return Ok(await service.GetPaged(new PagedAdvertsRequest
            {
                FollowerId = FollowerId,
                Limit = Limit,
                Offset = Offset,
                OwnerId = OwnerId
            }, cancellationToken));
        }
    }
}