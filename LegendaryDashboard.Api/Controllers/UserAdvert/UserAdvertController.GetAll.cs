using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.Contracts.UserAdvert;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        [HttpGet("all")]
        public List<UserAdvertDto> Get()
        {
            return _db.UserAdverts.Select(x => x.ToDto()).ToList();
        }
    }
}