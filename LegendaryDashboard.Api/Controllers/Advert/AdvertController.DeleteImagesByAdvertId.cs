using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpDelete("image/delete/advertId")]
        public async Task<IActionResult> DeleteImagesByAdvertId(
            int advertId,
            CancellationToken cancellationToken)
        {
            await _advertService.DeleteImagesByAdvertId(advertId, cancellationToken);
            return Ok();
        }
    }
}