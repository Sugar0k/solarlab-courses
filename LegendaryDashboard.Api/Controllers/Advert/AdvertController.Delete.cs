using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController : ControllerBase
    {
        [Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            await service.Delete(id, cancellationToken);
            return Ok();
        }
    }
}