using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpPost("images/add")]
        public async Task<IActionResult> AddImages(
            [FromForm] AdvertImageCreateRequest request,
            CancellationToken cancellationToken)
        {
            await _advertService.AddImages(request, cancellationToken);
            return Ok();
        }
    }
}