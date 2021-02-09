using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.AdvertImage
{

    partial class AdvertImageController
    {
        [HttpDelete]
        [Route("deleteById")]
        public IActionResult DeleteById(int id)
        {
            var adImage = _db.AdvertImages.FirstOrDefault(c => c.Id == id);
            
            if (adImage == null) 
                return BadRequest();
            
            var image = _db.Images.FirstOrDefault(c => c.Guid == adImage.ImageGuid);

            if (image != null) {
                System.IO.File.Delete(Path.Combine(image.FilePath, image.Guid));
            }
            _db.AdvertImages.Remove(adImage);
            _db.SaveChangesAsync();
            
            return Ok();
        }
    }
}