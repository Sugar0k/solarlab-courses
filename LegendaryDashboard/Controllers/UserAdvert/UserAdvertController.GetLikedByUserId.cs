using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.UserAdvert;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        [HttpGet("Liked/{id}")]
        public IEnumerable<UserAdvertDto> GetByLikedUserId(int id)
        {
            return _db.UserAdverts.Where(c => c.UserId == id && c.TypeId == 2).Select(x=>x.ToDto()).ToList();
        }
    }
}