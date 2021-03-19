using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpGet("image/get/advertId")]
        public async Task<IActionResult> GetImagesByAdvertId(
            int advertId,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            return Ok(await service.GetImagesByAdvertId(advertId, cancellationToken));
        }
    }
}