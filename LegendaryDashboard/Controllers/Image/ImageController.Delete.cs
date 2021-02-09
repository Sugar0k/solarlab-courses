using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.Image
{
    public partial class ImageController
    {
        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(string guid)
        {
            var image = _db.Images.Find(guid);
            
            if (image == null) return BadRequest();
            System.IO.File.Delete(Path.Combine(image.FilePath, guid));
            
            _db.Images.Remove(image);
            _db.SaveChangesAsync();
            
            return Ok();
        }
    }
}