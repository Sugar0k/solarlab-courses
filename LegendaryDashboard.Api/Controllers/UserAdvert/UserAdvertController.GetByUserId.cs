using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.UserAdvert;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        [HttpGet("user_id/{id}")]
        public IEnumerable<UserAdvertDto> GetByUserId(int id)
        {
            return _db.UserAdverts.Where(c => c.UserId == id).Select(x=>x.ToDto()).ToList();
        }
    }
}