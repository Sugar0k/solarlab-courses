using System.Linq;
using LegendaryDashboard.Contracts;
using LegendaryDashboard.Contracts.Contracts.AdvertImage.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.AdvertImage
{
    public partial class AdvertImageController
    {
        [HttpPost]
        [Route("create")]
        public IActionResult Create(AdvertImageCreateRequest request)
        {
            if (_db.Images.Find(request.ImageGuid) == null ||
                _db.AdvertImages.FirstOrDefault(c => c.ImageGuid == request.ImageGuid) != null) { 
                return BadRequest();
            }
            
            var adImage = new Domain.Models.AdvertImage
            {
                ImageGuid = request.ImageGuid,
                AdvertId = request.AdvertId
            };

            _db.AdvertImages.Add(adImage);
            _db.SaveChangesAsync();

            return Ok();
        }
    }
}