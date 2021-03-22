using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpPut("image/add")]
        public async Task<IActionResult> AddImage(
            int id,
            IFormFileCollection fileCollection,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            //TODO: Доделать под IFormFileCollection 
            await service.AddImage(id, fileCollection[0], cancellationToken);
            return Ok();
        }
    }
}