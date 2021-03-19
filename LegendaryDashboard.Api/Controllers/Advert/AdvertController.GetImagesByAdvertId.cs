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
        [HttpGet("get/image/advert")]
        public async Task<IActionResult> GetImagesByAdvertId(
            int advertId,
            [FromServices] IAdvertService service,
            CancellationToken cancellationToken)
        {
            var images = await service.GetImagesByAdvertId(advertId, cancellationToken);
            var files = new List<File>();
            foreach (var image in images)
            {
                byte[] mas = image.data;
                string fileType = "image/jpeg";
                string fileName = image.FileName;
                files.Add(File(mas, fileType, fileName));
            }
            return Ok(files);
        }
    }
}