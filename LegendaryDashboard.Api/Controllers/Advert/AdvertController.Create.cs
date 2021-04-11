using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.Advert.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(
            [FromForm] CreateAdvertRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _advertService.Create(request, cancellationToken));
        }
    }
}