using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.User;
using LegendaryDashboard.Controllers.Role;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("id/{id}")]
        public UserDto GetById(int id)
        {
            return _db.Users.FirstOrDefault(c => c.Id == id).ToDto();
        }
    }
}