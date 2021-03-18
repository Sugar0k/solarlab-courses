using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.AdvertService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Advert
{
    public partial class AdvertController
    {
        [HttpGet("get/image/")]
        public async Task<IActionResult> GetImageById(
            int advertId,
            string imageId,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            var image = await service.GetImage(advertId, imageId, cancellationToken);
            byte[] mas = image.data;
            string fileType = "image/jpeg";
            string fileName = image.FileName;
            return File(mas, fileType, fileName);
        }
    }
}