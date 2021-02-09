using System.Collections.Generic;
using System.IO;
using System.Linq;
using LegendaryDashboard.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.AdvertImage
{
    public partial class AdvertImageController
    {
        [HttpPost]
        [Route("get")]
        public IEnumerable<AdvertImageDto.AdvertImageDto> Get(int advertId)
        {
            return _db.AdvertImages.Where(c => c.AdvertId == advertId)
                .Select(adImage => new AdvertImageDto.AdvertImageDto()
            {
                id = adImage.Id,
                ImageGuid = adImage.ImageGuid,
                AdvertId = adImage.AdvertId,
            });
        }
    }
}