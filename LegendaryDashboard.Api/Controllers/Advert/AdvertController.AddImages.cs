using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpPut("images/add")]
        public async Task<IActionResult> AddImages(
            int id,
            IFormFileCollection fileCollection,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            await service.AddImages(id, fileCollection, cancellationToken);
            return Ok();
        }
    }
}