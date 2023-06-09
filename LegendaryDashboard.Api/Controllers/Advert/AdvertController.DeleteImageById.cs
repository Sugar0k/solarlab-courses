﻿using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpDelete("image/delete/imageId")]
        public async Task<IActionResult> DeleteImageById(
            string imageId,
            CancellationToken cancellationToken)
        {
            await _advertService.DeleteImage(imageId, cancellationToken);
            return Ok();
        }
    }
}