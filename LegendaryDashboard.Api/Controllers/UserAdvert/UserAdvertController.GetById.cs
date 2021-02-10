using System.Linq;
using LegendaryDashboard.Contracts.Contracts.UserAdvert;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.UserAdvert
{
    public partial class UserAdvertController
    {
        [HttpGet("id/{id}")]
        public UserAdvertDto GetById(int id)
        {
            return _db.UserAdverts.FirstOrDefault(c => c.Id == id).ToDto();
        }
    }
}