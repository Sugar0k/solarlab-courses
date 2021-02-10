using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.AdvertImage;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.AdvertImage
{
    public partial class AdvertImageController
    {
        [HttpPost]
        [Route("get")]
        public IEnumerable<AdvertImageDto> Get(int advertId)
        {
            return _db.AdvertImages.Where(c => c.AdvertId == advertId)
                .Select(adImage => new AdvertImageDto()
            {
                id = adImage.Id,
                ImageGuid = adImage.ImageGuid,
                AdvertId = adImage.AdvertId,
            });
        }
    }
}