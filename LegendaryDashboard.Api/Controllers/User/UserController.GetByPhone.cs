using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Contracts.Contracts.User;
using LegendaryDashboard.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [Authorize(Roles = RoleConstants.AdminRole)]
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(
            string phone,
            CancellationToken cancellationToken
        )
        {
            var user = await _userService.GetByPhone(phone, cancellationToken);
            return Ok(user);
        }
    }
}