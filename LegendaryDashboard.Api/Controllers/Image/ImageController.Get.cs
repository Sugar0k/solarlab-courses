using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.Image
{
    public partial class ImageController
    {
        [HttpPost]
        [Route("get")]
        public IActionResult Get(string guid)
        {
            Domain.Models.Image image = _db.Images.Find(guid);
            if (image == null) return BadRequest();
            byte[] mas = System.IO.File.ReadAllBytes(Path.Combine(image.FilePath, image.Guid));
            string fileType = "image/jpeg";
            string fileName = image.FileName;
            return File(mas, fileType, fileName);
        }
    }
}