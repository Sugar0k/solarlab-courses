using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("all")]
        public List<UserDto> Get()
        {
            return _db.Users.Select(x => x.ToDto()).ToList();
        }
    }
}