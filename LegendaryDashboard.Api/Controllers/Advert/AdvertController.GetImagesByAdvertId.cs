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
        [HttpGet("images/get/advertId")]
        public async Task<IActionResult> GetImagesByAdvertId(
            int advertId,
            CancellationToken cancellationToken)
        {
            return Ok(await _advertService.GetImagesByAdvertId(advertId, cancellationToken));
        }
    }
}