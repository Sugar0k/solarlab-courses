using System.Collections.Generic;
using System.Linq;
using LegendaryDashboard.Contracts.User;
using LegendaryDashboard.Controllers.Role;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("email/{email}")]
        public UserDto GetByEmail(string email)
        {
            return _db.Users.FirstOrDefault(c => c.Email == email).ToDto();
        }
    }
}