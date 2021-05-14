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
        [AllowAnonymous]
        [HttpGet("advertId/{id}")]
        public async Task<IActionResult> GetByAdvertId(
            int id,
            CancellationToken cancellationToken
        )
        {
            var phone = await _userService.FindByAdvertId(id, cancellationToken);
            return Ok(phone);
        }
    }
}