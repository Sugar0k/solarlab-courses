using System.Linq;
using LegendaryDashboard.Contracts.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Controllers.User
{
    public partial class UserController
    {
        [HttpGet("phone/{phone}")]
        public UserDto GetByPhone(string phone)
        {
            return _db.Users.FirstOrDefault(c => c.Phone == phone).ToDto();
        }
    }
}