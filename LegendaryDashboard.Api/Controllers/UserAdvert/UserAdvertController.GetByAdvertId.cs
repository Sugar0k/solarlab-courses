using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.UserAdvert;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        [HttpGet("advert_id/{id}")]
        public IEnumerable<UserAdvertDto> GetByAdvertId(int id)
        {
            return _db.UserAdverts.Where(c => c.AdvertId == id).Select(x=>x.ToDto()).ToList();
        }
    }
}