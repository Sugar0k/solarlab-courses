using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpGet("image/get/imageId")]
        public async Task<IActionResult> GetImageById(
            string imageId,
            CancellationToken cancellationToken)
        {
            var image = await _advertService.GetImage(imageId, cancellationToken);
            return File(image.data, "image/jpeg", image.FileName);
        }
    }
}