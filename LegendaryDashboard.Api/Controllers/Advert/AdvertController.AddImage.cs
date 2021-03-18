using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpPut("get/add/image")]
        public async Task<IActionResult> AddImage(
            int id,
            IFormFile file,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            await service.AddImage(id, file, cancellationToken);
            return Ok();
        }
    }
}