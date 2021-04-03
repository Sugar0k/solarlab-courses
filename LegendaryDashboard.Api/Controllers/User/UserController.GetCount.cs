using System.Threading;
using System.Threading.Tasks;
using LegendaryDashboard.Application.Services.UserService.Interfaces;
using LegendaryDashboard.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryDashboard.Api.Controllers.User
{
    public partial class UserController
    {
        [Authorize(Roles = RoleConstants.AdminRole)]
        [HttpGet("count")]
        public async Task<IActionResult> GetCount(
            CancellationToken cancellationToken
        )
        {
            var count = await _userService.Count(cancellationToken);
            return Ok(count);
        }
    }
}